using Microsoft.EntityFrameworkCore;
using Weather.RestApi.Data;

namespace Weather.RestApi.Config;

public static class DatabaseConfig {
	public static async Task ConfigureDatabase(this WebApplicationBuilder builder) {
		builder.Services.AddDbContext<IdentityDatabaseContext>(options => {
			// MSSQL: "Server=host.docker.internal,1433;Database=Weather_Development;User Id=sa;Password=P455w0r7!;TrustServerCertificate=true;"
			// PostgreSQL: "User ID=root;Password=password;Host=host.docker.internal;Port=5432;Database=Weather_Development;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;"
			options.UseNpgsql(
				"User ID=root;Password=password;Host=host.docker.internal;Port=5432;Database=Weather_Development;");
		});

		/*var context = builder.Services.BuildServiceProvider().GetRequiredService<IdentityDatabaseContext>();
		await context.Database.EnsureDeletedAsync();
		await context.Database.EnsureCreatedAsync();*/
	}
}