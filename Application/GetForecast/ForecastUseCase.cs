using System.Text.Json;
using System.Text.Json.Serialization;
using Entities;
using ShuttleForecast.Application.Common.Contracts;
using ShuttleForecast.Application.Common.Models;

namespace ShuttleForecast.Application.GetForecast;

public class ForecastUseCase(
    IForecastProviderClient forecastProviderClient,
    IForecastRepository forecastRepository,
    ICache cache)
    : IForecastUseCase
{
    public async Task<object?> GetForecastAsync(decimal latitude, decimal longitude,
        CancellationToken cancellationToken)
    {
        var cacheResult = cache.GetData($"latitude={latitude}&longitude={longitude}");
        if (!string.IsNullOrWhiteSpace(cacheResult))
            return cacheResult;

        var weather = await forecastProviderClient.GetForecastAsync(latitude, longitude, cancellationToken);

        if (weather == null)
        {
            var latestForecast =
                await forecastRepository.GetLatestForecastAsync(latitude, longitude, cancellationToken);
            return latestForecast;
        }

        await InsertForecast(weather, cancellationToken);
        cache.SetData($"latitude={latitude}&longitude={longitude}", JsonSerializer.Serialize(weather));

        return weather;
    }

    private async Task InsertForecast(ForecastProviderResponse weather, CancellationToken cancellationToken)
    {
        var forecast = new Forecast
        {
            Latitude = weather.Latitude,
            Longitude = weather.Longitude,
            Data = JsonSerializer.Serialize(weather)
        };

        await forecastRepository.InsertForecastAsync(forecast, cancellationToken);
        await forecastRepository.SaveAsync(cancellationToken);
    }
}