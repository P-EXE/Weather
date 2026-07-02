using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Weather.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserManagementController(UserManager<IdentityUser> userManager) : Controller {
	private UserManager<IdentityUser> _userManager = userManager;

	[HttpGet("self")]
	public async Task<ActionResult<object>> GetSelf() {
		var cpUser = User;
		var umUser = await _userManager.GetUserAsync(User);
		return Ok(new {
			cpUser,
			umUser
		});
	}
}