using System.Net;
using System.Net.Http.Json;
using Weather.RestApi.Tests.IntegrationTests.Setup;

namespace Weather.API.REST.Tests.Unit.IntegrationTests;

[Collection("WithDefaultFixture")]
public class IdentityTests(Fixture fixture) {
	private Fixture _fixture = fixture;

	[Fact]
	public async Task Register_Valid() {
		// Arrange
		Dictionary<string, string> credentials = new()
		{
			["Email"] = "testuser@mail.com",
			["Password"] = "P455w0rd!",
		};
		// Act
		HttpResponseMessage response = await _fixture.Client.PostAsJsonAsync("/identity/register", credentials, _fixture.JsonOpt);

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}
}