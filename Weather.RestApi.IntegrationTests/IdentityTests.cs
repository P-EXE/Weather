using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Weather.RestApi.Data;

namespace Weather.RestApi.IntegrationTests;

public class IdentityTests(WeatherApiFactory weatherApiFactory) : IClassFixture<WeatherApiFactory> {
	private readonly HttpClient _client = weatherApiFactory.CreateClient();
	private readonly JsonSerializerOptions _jsonOpt = new() {
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase
	};

	[Fact]
	public async Task Register_Valid() {
		// Arrange
		Dictionary<string, string> credentials = new() {
			["Email"] = "testuser@mail.com",
			["Password"] = "P455w0rd!",
		};
		// Act
		HttpResponseMessage response = await _client.PostAsJsonAsync("/identity/register", credentials, _jsonOpt);

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}

	[Fact]
	public async Task Login_Valid() {
		// Arrange
		Dictionary<string, string> credentials = new() {
			["Email"] = "testuser@mail.com",
			["Password"] = "P455w0rd!",
		};
		// Act
		HttpResponseMessage response = await _client.PostAsJsonAsync("/identity/login", credentials, _jsonOpt);

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		var result = await response.Content.ReadFromJsonAsync<AccessTokenResponse>(_jsonOpt);
		Assert.False(string.IsNullOrEmpty(result!.AccessToken));
		var jwtHandler = new JsonWebTokenHandler();
		var jwt = jwtHandler.ReadJsonWebToken(result.AccessToken);
		Assert.Contains(jwt.Claims, c => c.Type == ClaimTypes.Email && c.Value == "testuser@mail.com");
	}
}