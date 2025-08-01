namespace Entities;

public record Forecast
{
    public int Id { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string Data { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}