namespace WeatherShared.DTOs;

public record RegisterEmailDTO(
  string Email,
  string Password,
  string FirstName,
  string LastName,
  AddressCreateDTO Address
);

public record LoginEmailDTO(
  string Email,
  string Password
);

public record LoginResponseDTO(string Token);
