using Microsoft.AspNetCore.Identity;

namespace WeatherApi.Models;

public class UserModel : IdentityUser<Guid> {
  public string FirstName { get; set; }
  public string LastName { get; set; }
}
