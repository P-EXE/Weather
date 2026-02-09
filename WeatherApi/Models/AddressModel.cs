namespace WeatherApi.Models;

public class AddressModel {
  public Guid Id { get; set; }
  public string Address { get; set; }
  public UserModel User { get; set; }
  public Guid UserId { get; set; }
}
