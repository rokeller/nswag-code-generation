using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Cli.Services.Remote;

namespace Sample.Cli;

internal sealed class Worker : BackgroundService
{
    private readonly IHostApplicationLifetime lifetime;
    private readonly IForecastService forecastService;
    private readonly ILogger<Worker> logger;

    public Worker(
        ILogger<Worker> logger,
        IHostApplicationLifetime lifetime,
        IForecastService forecastService)
    {
        this.lifetime = lifetime;
        this.forecastService = forecastService;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogDebug("Invoking the forecast service ...");
        ICollection<Forecast> forecast = await forecastService.GetForecastAsync(stoppingToken).ConfigureAwait(false);

        foreach (Forecast f in forecast)
        {
            logger.LogInformation("Forecast for {date:yyyy-MM-dd}:\n\t{summary}, {temperatureC} degrees Celsius",
                f.Date, f.Summary, ToCelsius(f));
        }

        logger.LogInformation("Done.");
        lifetime.StopApplication();
    }

    private static int ToCelsius(Forecast forecast)
    {
        return forecast.TemperatureK - 273;
    }
}
