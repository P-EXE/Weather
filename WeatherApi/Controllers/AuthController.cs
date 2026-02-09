using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;
using WeatherShared.DTOs;

namespace WeatherApi.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class AuthController(
	UserManager<UserModel> userManager,
	SignInManager<UserModel> signinManager
	) : ControllerBase {

	[HttpPost("register-email")]
	public async Task<ActionResult<LoginResponseDTO>> RegisterEmail(RegisterEmailDTO credentials) {
	  var user = new UserModel {
		UserName = credentials.Email,
		Email = credentials.Email,
		FirstName = credentials.FirstName,
		LastName = credentials.LastName,
	  };
	  var result = await userManager.CreateAsync(user, credentials.Password);
	  if(!result.Succeeded) return BadRequest(result.Errors);
	  await signinManager.SignInAsync(user, isPersistent: false);
	  return Ok("Fake Token!");
	}
	[HttpPost("login-email")]
	public ActionResult<LoginResponseDTO> LoginEmail(LoginEmailDTO credentials) {
	  return Ok(new LoginResponseDTO("Fake Token!"));
	}
  }
}
