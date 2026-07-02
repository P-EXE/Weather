using Weather.RestApi.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

switch (builder.Environment.EnvironmentName) {
	case "Development": {
		builder.ConfigureDebugTools();
		await builder.ConfigureDatabase();
		builder.ConfigureAuth();
		break;
	}
	case "Staging": {
		break;
	}
	case "Production": {
		builder.ConfigureDebugTools();
		await builder.ConfigureDatabase();
		builder.ConfigureAuth();
		break;
	}
}

var app = builder.Build();
app.MapControllers();

switch (app.Environment.EnvironmentName) {
	case "Development": {
		app.UseDebugTools();
		app.UseAuth();
		break;
	}
	case "Staging": {
		break;
	}
	case "Production": {
		app.UseHttpsRedirection();
		app.UseDebugTools();
		app.UseAuth();
		break;
	}
}

app.Run();