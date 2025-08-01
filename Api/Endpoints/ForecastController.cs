using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShuttleForecast.Application.Common.Models;
using ShuttleForecast.Application.GetForecast;

namespace ShuttleForecast.Api.Endpoints;

[Route("/api/v1/forecast")]
[AllowAnonymous]
public class ForecastController(IForecastUseCase forecastUseCase) : ControllerBase
{
    [HttpGet]
    public async Task<object?> GetWeatherForecast([FromQuery] decimal latitude = 52.52m,
        [FromQuery] decimal longitude = 13.41m)
    {
        return await forecastUseCase.GetForecastAsync(latitude, longitude, HttpContext.RequestAborted);
    }
}