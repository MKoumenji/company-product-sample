
using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient;
using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Interfaces;
using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Models;
using Company.Product.Infrastructure.Repository.ExternalServices.RestClient;
using Company.Product.Infrastructure.Repository.Persistence.SqlDb;
using Company.Product.Infrastructure.Repository.WebIntegration.WebAppManager;
using Company.Product.Infrastructure.Services.Logging;
using Company.Product.Infrastructure.Services.Notification;
using Company.Product.Infrastructure.Services.OrderProcessing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;

namespace Company.Product.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddThirdPartyServices(configuration);
        services.AddEmailServices(configuration);
        services.AddSerilog(configuration);
        return services;
    }
    
    private static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IDbLoggerService, DbLogger>();
        services.AddSingleton<INotifierService, Notifier>();
        services.AddSingleton<IOrderHandler, OrderHandler>();
        services.AddSingleton<IRetrieveLoggedOrders, LoggedOrders>();
        services.AddSingleton<IRetrieveProcessedOrders, ProcessedOrders>();
        services.AddSingleton<ISecLoggerService, SecLoggerService>();
        services.AddSingleton <IOrderHandlerHistory, OrderHandlerHistory>();
    }
    
    private static void AddThirdPartyServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IisConfig>(configuration.GetSection(nameof(IisConfig)));
        
        var iisConfig = configuration.GetSection(nameof(IisConfig)).Get<IisConfig>();
        
        if (iisConfig == null || string.IsNullOrEmpty(iisConfig.SiteName))
        {
            throw new InvalidOperationException("IisConfig is not configured properly. Please check your configuration.");
        }
        
        services.AddSingleton<ISqlDb>(provider =>
        {
            var requiredService = provider.GetRequiredService<IConfiguration>();
            var connectionString = requiredService.GetConnectionString("CurrentDB");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'CurrentDB' is missing!");
            }
            return new SqlDb(connectionString);
        });

        services.AddSingleton<IWebAppManagerIis, WebAppManagerIis>();
        services.AddHttpClient<IRestClient, RestClient>(httpClient => httpClient.Timeout = Timeout.InfiniteTimeSpan);
    }

    private static void AddEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailKitConfig>(configuration.GetSection(nameof(MailKitConfig)));
        var mailKitConfig = configuration.GetSection(nameof(MailKitConfig)).Get<MailKitConfig>();
        if (mailKitConfig == null || string.IsNullOrEmpty(mailKitConfig.SmtpServer))
        {
            throw new InvalidOperationException("MailKitConfig is not configured properly. Please check your configuration.");
        }
        
        services.AddSingleton<IEmailSender, EmailSender>();
        services.AddSingleton<IEmailMessageBuilder, EmailMessageBuilder>();
    }

    
    private static void AddSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration) // serilog .setting . configuration 
            .Enrich
            .WithExceptionDetails(
                new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
            )
            .Enrich.WithAssemblyName()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .Enrich.WithEnvironmentName()
            .CreateLogger();
        
        services.AddSingleton(Log.Logger);

    }

}