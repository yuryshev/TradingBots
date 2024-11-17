using ExchangeAdapters.Core.Endpoints;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddHttpClient();

        services.AddScoped<IBinanceSpotEndpoints, BinanceSpotEndpoints>();
    })
    .Build();

host.Run();
