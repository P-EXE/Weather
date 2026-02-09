using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeatherApi.Database;
using WeatherApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Todo
builder.Services.AddOpenApi();

builder.Services.AddAuthorization();
await builder.Services.AddSQLServerContainer();
builder.Services.AddIdentityApiEndpoints<UserModel>(options =>
  options.SignIn.RequireConfirmedAccount = false
).AddEntityFrameworkStores<DatabaseContext>();
builder.Services.Configure<IdentityOptions>(options => {
  // Password settings.
  options.Password.RequireDigit = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireNonAlphanumeric = true;
  options.Password.RequireUppercase = true;
  options.Password.RequiredLength = 6;
  options.Password.RequiredUniqueChars = 1;
  // Lockout settings.
  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
  options.Lockout.MaxFailedAccessAttempts = 5;
  options.Lockout.AllowedForNewUsers = true;
  // User settings.
  options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
  options.User.RequireUniqueEmail = false;
});

switch(builder.Environment.EnvironmentName) {
  case "Development": {
	  builder.Services.AddEndpointsApiExplorer();
	  builder.Services.AddSwaggerGen();
	  break;
	}
  case "Staging": {
	  break;
	}
  case "Production": {
	  break;
	}
  default:
	break;
}

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapIdentityApi<UserModel>();

switch(app.Environment.EnvironmentName) {
  case "Development": {
	  app.MapOpenApi();
	  app.UseSwagger();
	  app.UseSwaggerUI(options => {
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
		options.RoutePrefix = string.Empty;
	  });
	  break;
	}
  case "Staging": {
	  break;
	}
  case "Production": {
	  break;
	}
  default:
	break;
}

app.Run();
