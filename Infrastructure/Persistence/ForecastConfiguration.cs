using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence;

public class ForecastConfiguration : IEntityTypeConfiguration<Forecast>
{
    public void Configure(EntityTypeBuilder<Forecast> builder)
    {
        builder.ToTable("Forecasts");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Latitude)
            .IsRequired();

        builder.Property(f => f.Longitude)
            .IsRequired();

        builder.Property(f => f.CreatedDate)
            .IsRequired();

        builder.Property(f => f.Data)
            .IsRequired();
    }
}