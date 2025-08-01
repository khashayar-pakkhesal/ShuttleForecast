using ShuttleForecast.Application.Common.Models;

namespace ShuttleForecast.Application.Common.Contracts;

public interface IForecastProviderClient
{
    public Task<ForecastProviderResponse?> GetForecastAsync(decimal latitude, decimal longitude,
        CancellationToken cancellationToken);
}