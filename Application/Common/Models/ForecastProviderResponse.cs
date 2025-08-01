using System.Text.Json.Serialization;

namespace ShuttleForecast.Application.Common.Models;

public class ForecastProviderResponse
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public double GenerationtimeMs { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string? Timezone { get; set; }
    public string? TimezoneAbbreviation { get; set; }
    public double Elevation { get; set; }

    [JsonPropertyName("hourly_units")] public HourlyUnit? HourlyUnits { get; set; }
    public HourlyData? Hourly { get; set; }


    public class HourlyUnit
    {
        public string? Time { get; set; }

        [JsonPropertyName("temperature_2m")] public string? Temperature2M { get; set; }
    }

    public class HourlyData
    {
        public List<string> Time { get; set; } = new();
        public List<double> Temperature2M { get; set; } = new();
    }
}