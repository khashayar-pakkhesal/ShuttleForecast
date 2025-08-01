using Entities;
using Microsoft.EntityFrameworkCore;
using ShuttleForecast.Application.Common.Contracts;

namespace Infrastructure.Persistence;

public class ForecastRepository(ForecastDbContext dbContext) : IForecastRepository
{
    private const decimal Tolerance = 0.1m;

    public async Task<Forecast?> GetLatestForecastAsync(decimal latitude, decimal longitude,
        CancellationToken cancellationToken)
    {
        return await dbContext.Forecasts.Where(x => Math.Abs(x.Longitude - longitude) < Tolerance
                                                    && Math.Abs(x.Latitude - latitude) < Tolerance)
            .OrderByDescending(x => x.CreatedDate)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task InsertForecastAsync(Forecast forecast, CancellationToken cancellationToken)
    {
        await dbContext.Forecasts.AddAsync(forecast, cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}