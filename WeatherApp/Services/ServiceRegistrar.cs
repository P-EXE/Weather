using WeatherApp.ViewModels;
using WeatherApp.Views;

namespace WeatherApp.Services;

public static class ServiceRegistrar {
	extension(IServiceCollection services) {
		public void Register() {
			services.RegisterViews();
			services.RegisterViewModels();
		}

		private void RegisterViews() {
			services.AddTransient<LoginPage>();
		}

		private void RegisterViewModels() {
			services.AddTransient<LoginVM>();
		}
		
		private void RegisterServices() {
			services.AddTransient<LoginVM>();
		}
	}
}