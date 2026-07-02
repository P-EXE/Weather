using System.Data.Common;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Weather.API.REST.Data;

namespace Weather.API.REST.Tests.Unit.IntegrationTests.Setup;

public class Fixture : WebApplicationFactory<Program> {
	public readonly HttpClient Client;
	public readonly JsonSerializerOptions JsonOpt;

	public Fixture() {
		Client = CreateClient();

		JsonOpt = new() {
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
	}

	protected override void ConfigureWebHost(IWebHostBuilder builder) {
		base.ConfigureWebHost(builder);
		builder.UseEnvironment("Development");
		builder.ConfigureTestServices(async void (services) => {
			await services.ConfigureTestDatabase();
			/*await services.ConfigureInMemoryTestDb();*/
		});
	}
}

internal static class AuthConfig {
	internal static void ConfigureTestAuth(this IServiceCollection services) {
		services.AddAuthentication()
			.AddBearerToken();

		services.AddAuthentication("token")
			.AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("token", null);

		services.AddAuthorization();
	}

	public class TestAuthenticationHandler(
		IOptionsMonitor<AuthenticationSchemeOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder) {
		protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
			IEnumerable<Claim> claims = [];
			ClaimsIdentity identity = new(claims, "token");
			ClaimsPrincipal principal = new(identity);
			AuthenticationTicket ticket = new(principal, "token");
			AuthenticateResult result = AuthenticateResult.Success(ticket);

			return result;
		}
	}
}

internal static class DatabaseConfig {
	internal static async Task ConfigureTestDatabase(this IServiceCollection services) {
		// Remove the regular DbContext
		ServiceDescriptor? dbContextDescriptor = services.SingleOrDefault(d =>
			d.ServiceType == typeof(DbContextOptions<IdentityDatabaseContext>)
		);
		services.Remove(dbContextDescriptor!);
		ServiceDescriptor? dbConnectionDescriptor = services.SingleOrDefault(d =>
			d.ServiceType == typeof(DbConnection)
		);
		services.Remove(dbConnectionDescriptor!);

		// Add test DbContext
		services.AddDbContext<IdentityDatabaseContext>(options => {
			options.UseSqlServer(
				"Server=host.docker.internal,1433;Database=Weather_Tests;User Id=sa;Password=P455w0r7!;TrustServerCertificate=true;");
			options.EnableDetailedErrors();
			options.EnableSensitiveDataLogging();
		});
		
		// Reset Db
		await using var dbContext = services.BuildServiceProvider().CreateScope().ServiceProvider
			.GetRequiredService<IdentityDatabaseContext>();
		await dbContext.Database.EnsureDeletedAsync();
		await dbContext.Database.EnsureCreatedAsync();
	}

	internal static async Task ConfigureInMemoryTestDatabase(this IServiceCollection services) {
		services.AddSingleton<DbConnection>(container => {
			SqliteConnection connection = new("Data Source=Weather_Tests;Mode=Memory;Cache=Shared");
			connection.Open();

			return connection;
		});

		services.AddDbContext<IdentityDatabaseContext>((services, options) => {
			DbConnection connection = services.GetRequiredService<DbConnection>();
			options.UseSqlite(connection);
			options.EnableDetailedErrors();
			options.EnableSensitiveDataLogging();
		});

		await using var context = services.BuildServiceProvider().GetRequiredService<IdentityDatabaseContext>();
		await context.Database.EnsureDeletedAsync();
		await context.Database.EnsureCreatedAsync();
	}
