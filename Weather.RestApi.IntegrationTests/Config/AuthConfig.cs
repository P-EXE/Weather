using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

internal static class AuthConfig {
	internal static void ConfigureTestAuth(this IServiceCollection services) {
		services.AddAuthentication()
			.AddBearerToken();

		services.AddAuthentication("token")
			.AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("token", null);

		services.AddAuthorization();
	}

	public class TestAuthenticationHandler(
		IOptionsMonitor<AuthenticationSchemeOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder) {
		protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
			IEnumerable<Claim> claims = [];
			ClaimsIdentity identity = new(claims, "token");
			ClaimsPrincipal principal = new(identity);
			AuthenticationTicket ticket = new(principal, "token");
			AuthenticateResult result = AuthenticateResult.Success(ticket);

			return result;
		}
	}
}