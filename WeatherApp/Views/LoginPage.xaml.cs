using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}