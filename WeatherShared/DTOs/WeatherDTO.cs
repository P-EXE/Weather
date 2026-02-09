namespace WeatherShared.DTOs;

public record WeatherDTO(
  DateOnly Date,
  int Temperature,
  string? Summary
);
