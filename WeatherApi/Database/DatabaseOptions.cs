using Microsoft.EntityFrameworkCore;

namespace WeatherApi.Database;

public static class DatabaseOptions {
  public static string SQLServerConnectionString = Environment.GetEnvironmentVariable("SQLSERVER_SERVER")!;

  public static async Task AddSQLServerContainer(this IServiceCollection services) {
	services.AddDbContext<DatabaseContext>(options =>
	  options.UseSqlServer(SQLServerConnectionString)
	);

	var context = services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
	await context.Database.EnsureDeletedAsync();
	await context.Database.EnsureCreatedAsync();
  }

}
