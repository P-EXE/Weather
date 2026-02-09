using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WeatherApp.ViewModels;

public partial class LoginVM : ObservableObject
{
  [RelayCommand]
  private void LoginEmail() { }

  [RelayCommand]
  private async Task LoginGoogle()
  {
	try
	{
	  WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
		  new Uri("https://mysite.com/mobileauth/Microsoft"),
		  new Uri("myapp://"));

	  string accessToken = authResult?.AccessToken;

	  // Do something with the token
	}
	catch (TaskCanceledException e)
	{
	  // Use stopped auth
	}
  }
}