using Weather.RestApi.Controllers;
using WeatherShared.DTOs;

namespace Weather.API.REST.Tests.Unit;

public class WeatherControllerTests() {
	[Fact]
	public void Get_Valid() {
		var weatherController = new WeatherController();
		string[] validSummaries = [
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		];

		var response = weatherController.Get();
		
		Assert.NotNull(response);
		List<WeatherDTO> weathers = response.ToList();
		Assert.NotEmpty(weathers);
		for (int i = 0; i < weathers.Count; i++) {
			Assert.Equal(weathers[i].Date, DateOnly.FromDateTime(DateTime.Now.AddDays(i + 1)));
			Assert.InRange(weathers[i].Temperature, -20, 55);
			Assert.Contains(weathers[i].Summary, validSummaries);
		}
	}
}