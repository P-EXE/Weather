namespace WeatherApp.Services;

public class AuthService {
  public async Task<string?> GetAuthToken() =>
	await SecureStorage.Default.GetAsync("oauth_token");
}
