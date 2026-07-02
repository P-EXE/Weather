namespace Weather.RestApi.Config;

public static class DebugToolsConfig {
	public static void ConfigureDebugTools(this WebApplicationBuilder builder) {
		builder.Services.AddOpenApi();
		builder.Services.AddEndpointsApiExplorer();
		// Swagger
		builder.Services.AddSwaggerGen();
	}

	public static void UseDebugTools(this WebApplication app) {
		app.MapOpenApi();
		// Swagger
		app.UseSwagger();
		app.UseSwaggerUI(options => {
			options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
			options.RoutePrefix = string.Empty;
		});
	}
}