namespace Sample.Api.Models;

public partial class Forecast
{
    public DateTimeOffset Date { get; set; }

    public int TemperatureK { get; set; }

    public WeatherSummary Summary { get; set; }
}
