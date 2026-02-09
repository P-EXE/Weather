using Microsoft.EntityFrameworkCore;

namespace WeatherApi.Database;

public static class DatabaseOptions {
  /// <summary>
  /// Default: "Server=host.docker.internal,1433;Database=RIPD;User Id=sa;Password=P455w0rd!;TrustServerCertificate=true;"
  /// You need to pull the docker image & run the container manually 4 now!
  /// docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P455w0rd!" -e "MSSQL_PID=Express" -p 1433:1433 --name MSSQLServer -d mcr.microsoft.com/mssql/server:2022-latest
  /// </summary>
  public static string SQLServerConnectionString = Environment.GetEnvironmentVariable("SQLSERVER_SERVER") ??
	"Server=host.docker.internal,1433;Database=Weather;User Id=sa;Password=P455w0rd!;TrustServerCertificate=true;";

  public static async Task AddSQLServerContainer(this IServiceCollection services) {
	services.AddDbContext<DatabaseContext>(options =>
	  options.UseSqlServer(SQLServerConnectionString)
	);

	var context = services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
	await context.Database.EnsureDeletedAsync();
	await context.Database.EnsureCreatedAsync();
  }

}
