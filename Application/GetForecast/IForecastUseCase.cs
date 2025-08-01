namespace ShuttleForecast.Application.GetForecast;

public interface IForecastUseCase
{
    public Task<object?> GetForecastAsync(decimal latitude, decimal longitude,
        CancellationToken cancellationToken);
}