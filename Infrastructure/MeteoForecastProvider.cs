using System.Text.Json;
using ShuttleForecast.Application.Common.Contracts;
using ShuttleForecast.Application.Common.Models;

namespace Infrastructure;

public class MeteoForecastProvider(HttpClient httpClient) : IForecastProviderClient
{
    public async Task<ForecastProviderResponse?> GetForecastAsync(decimal latitude, decimal longitude,
        CancellationToken cancellationToken)
    {
        var url =
            $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m";

        try
        {
            var response = await httpClient.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            var result = JsonSerializer.Deserialize<ForecastProviderResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result;
        }
        catch (Exception _)
        {
            return null;
        }
    }
}