using Entities;

namespace ShuttleForecast.Application.Common.Contracts;

public interface IForecastRepository
{
    public Task<Forecast?> GetLatestForecastAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken);
    public Task InsertForecastAsync(Forecast forecast, CancellationToken cancellationToken);
    public Task SaveAsync(CancellationToken cancellationToken);
}