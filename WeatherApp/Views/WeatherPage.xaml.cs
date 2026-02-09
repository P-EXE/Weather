using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class WeatherPage : ContentPage {
  public WeatherPage(WeatherVM vm) {
	InitializeComponent();
	BindingContext = vm;
  }
}