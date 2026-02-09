using WeatherApp.ViewModels;
using WeatherApp.Views;

namespace WeatherApp.Services;

public static class ServiceRegistrar {
  extension(IServiceCollection services) {
	public void Register() {
	  services.RegisterViews();
	  services.RegisterViewModels();
	  services.RegisterServices();
	  RegisterRoutes();
	}

	private void RegisterViews() {
	  services.AddTransient<AuthPage>();
	  services.AddTransient<LoginPage>();
	  services.AddTransient<WeatherPage>();
	}

	private void RegisterViewModels() {
	  services.AddTransient<LoginVM>();
	  services.AddTransient<WeatherVM>();
	}

	private void RegisterServices() {
	  services.AddTransient<AuthService>();
	}

	private static void RegisterRoutes() {
	  Routing.RegisterRoute(Routes.AuthPage, typeof(AuthPage));
	  Routing.RegisterRoute(Routes.LoginPage, typeof(LoginPage));
	  Routing.RegisterRoute(Routes.WeatherPage, typeof(WeatherPage));
	}
  }
}