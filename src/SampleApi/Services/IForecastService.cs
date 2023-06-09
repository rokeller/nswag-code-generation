using Sample.Api.Models;

namespace Sample.Api.Services;

public interface IForecastService
{
    Task<IEnumerable<Forecast>> GetForecastAsync();
}
