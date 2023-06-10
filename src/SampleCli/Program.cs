using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Cli;
using Sample.Cli.Services.Remote;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(ConfigureServices)
    .Build();

host.Run();

void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services
        .AddHttpClient<IForecastService, ForecastService>()
        .ConfigurePrimaryHttpMessageHandler(ConfigurePrimaryHttpHandler)
        .Services
        .AddHostedService<Worker>()
        ;
}

HttpMessageHandler ConfigurePrimaryHttpHandler()
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = ValidateServerCertificate,
    };
}

bool ValidateServerCertificate(
    HttpRequestMessage message,
    X509Certificate2? certificate,
    X509Chain? chain,
    SslPolicyErrors errors)
{
    // Don't do this unless you *really* understand the impact on security.
    return true;
}
