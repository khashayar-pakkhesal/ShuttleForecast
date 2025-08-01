using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ForecastDbContext(DbContextOptions<ForecastDbContext> options) : DbContext(options)
{
    public DbSet<Forecast> Forecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ForecastConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}