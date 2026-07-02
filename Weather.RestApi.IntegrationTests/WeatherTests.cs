using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Weather.RestApi.IntegrationTests;

public class WeatherTests : IClassFixture<WeatherApiFactory> {
	private readonly HttpClient _client;
	private readonly JsonSerializerOptions _jsonOpt;

	public WeatherTests(WeatherApiFactory weatherApiFactory) {
		_client = weatherApiFactory.CreateClient();

		_jsonOpt = new() {
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
	}

	[Fact]
	public async Task GetWeather_Valid() {
		// Act
		HttpResponseMessage response = await _client.GetAsync("/Weather/forecast");
		
		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}
}