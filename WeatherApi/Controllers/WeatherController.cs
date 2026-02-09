using Microsoft.AspNetCore.Mvc;
using WeatherShared.DTOs;

namespace WeatherApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherController : ControllerBase
  {
	private static readonly string[] Summaries =
	[
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	];

	[HttpGet("forecast")]
	public IEnumerable<WeatherDTO> Get()
	{
	  return [.. Enumerable.Range(1, 5).Select(index => new WeatherDTO(
		Date: DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
		Temperature: Random.Shared.Next(-20, 55),
		Summary: Summaries[Random.Shared.Next(Summaries.Length)]
	  ))];
	}
  }
}
