namespace ShuttleForecast.Application.GetForecast;

public interface IForecastUseCase
{
    public ValueTask<object?> GetForecastAsync(decimal latitude, decimal longitude,
        CancellationToken cancellationToken);
}