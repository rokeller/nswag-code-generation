using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Services;

namespace Sample.Api.Controllers.V1;

[ApiController]
public sealed class ForecastController : ForecastControllerBase
{
    private readonly IMapper mapper;
    private readonly IForecastService forecastService;

    public ForecastController(IMapper mapper, IForecastService forecastService)
    {
        this.mapper = mapper;
        this.forecastService = forecastService;
    }

    public override async Task<ActionResult<ICollection<Forecast>>> GetForecast()
    {
        IEnumerable<Forecast> forecast = (await forecastService.GetForecastAsync())
            .Select(f => mapper.Map<Forecast>(f));
        
        return Ok(forecast);
    }
}
