namespace ShuttleForecast.Application.Common.Contracts;

public interface ICache
{
    public string? GetData(string key);
    public void SetData(string key, string data);
}