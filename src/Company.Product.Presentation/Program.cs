using Company.Product.Application;
using Company.Product.Domain;
using Company.Product.Infrastructure;
using Company.Product.Presentation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        var environment = hostContext.HostingEnvironment.EnvironmentName;
        
        // Load environment-specific appsettings.{Environment}.json
        config
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })

    .ConfigureServices((hostContext, services) =>
    {
        
        // Bind the WorkerSettings from json
        services.AddApplicationLayer(hostContext.Configuration);
        services.AddInfrastructureLayer(hostContext.Configuration);
        services.AddHostedService<Worker>();

    })
    .UseWindowsService() // Add Sopprting Windows Service 
    .Build();

await host.RunAsync();
