using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Weather.RestApi.Data;

namespace Weather.RestApi.IntegrationTests;

public class WeatherApiFactory : WebApplicationFactory<Program>, IAsyncLifetime {
	private readonly PostgreSqlContainer _sql = new PostgreSqlBuilder()
		.WithDatabase("Weather_Tests")
		.WithUsername("root")
		.WithPassword("password")
		.Build();
	protected override void ConfigureWebHost(IWebHostBuilder builder) {
		builder.ConfigureTestServices(services => {
			services.Remove(services.Single(descriptor =>
				descriptor.ServiceType == typeof(DbContextOptions<IdentityDatabaseContext>)));
			services.AddDbContext<IdentityDatabaseContext>(options => { options.UseNpgsql(_sql.GetConnectionString()); });
		});
	}
	public async Task InitializeAsync() {
		await _sql.StartAsync();
		using var scope = Services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<IdentityDatabaseContext>();
		await context.Database.EnsureCreatedAsync();
	}

	public new async Task DisposeAsync() {
		await _sql.StopAsync();
	}
}