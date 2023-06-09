using Sample.Api.Models;

namespace Sample.Api.Services;

internal sealed class RandomForecastService : IForecastService
{
    public Task<IEnumerable<Forecast>> GetForecastAsync()
    {
        return Task.FromResult<IEnumerable<Forecast>>(
            Enumerable.Range(1, 5)
                .Select(offset => new Forecast()
                {
                    Date = DateTime.UtcNow.AddDays(offset).Date,
                    TemperatureK = Random.Shared.Next(253, 328),
                    Summary = (WeatherSummary)Random.Shared.Next((int)WeatherSummary.Scorching + 1),
                })
                .ToList());
    }
}
