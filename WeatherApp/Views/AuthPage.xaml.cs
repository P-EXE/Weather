using WeatherApp.Services;

namespace WeatherApp.Views;

public partial class AuthPage : ContentPage {
  private readonly AuthService _authService;
  public AuthPage(AuthService authService) {
	InitializeComponent();
	_authService = authService;
  }

  protected override async void OnAppearing() {
	base.OnAppearing();

	await Task.Delay(1);

	//if(_authService.GetAuthToken() is null) {
	  // User is not logged in, navigate to login page
	  await Shell.Current.GoToAsync($"///RegisterLogin");
	//}
  }
}