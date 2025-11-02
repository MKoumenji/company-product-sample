using System.Reflection;
using Company.Product.Application.DTOs.Config;
using Company.Product.Application.Pipeline;
using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Pipeline.Factory;
using Company.Product.Application.Pipeline.Operations;
using Company.Product.Application.UseCases;
using Company.Product.Application.UseCases.Abstarction;
using Company.Product.Domain.Contracts;
using Company.Product.Domain.DomainServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Product.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer( this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration
        services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));
        
        //Mediator
        services.AddMediatR(cfg =>
        { 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        // Domain Services
        services.AddSingleton<IOrderValidationService, OrderValidationService>();
        services.AddSingleton<IOrderAddAttributService, OrderAddAttributService>();
        services.AddSingleton<ISchedulerService, SchedulerService>();
        
        // Use Cases
        services.AddSingleton <IRetrieveOrders, RetrieveOrders>();
        services.AddSingleton <IDispatchPendingOrders, DispatchPendingOrders>();
        services.AddSingleton <IRestartOrderHandler, RestartOrderHandler>();
        services.AddSingleton <IValidateDispatchedOrders, ValidateDispatchedOrders>();
        services.AddSingleton <IReviewOrders, ReviewOrders>();
        
        // Pipeline Operations
        services.AddTransient<ScheduleCheckOperation>();
        services.AddTransient<RetrieveOrdersOperation>();
        services.AddTransient<ReviewOrdersOperation>();
        services.AddTransient<DispatchPendingOrdersOperation>();
        services.AddTransient<ValidateDispatchedOrdersOperation>();
        services.AddTransient<RestartOrderHandlerOperation>();

        
        
        // Pipeline Infrastructure
        services.AddTransient<IOrderProcessingPipelineFactory, OrderProcessingPipelineFactory>();
        services.AddTransient<IOperationProcessing, OperationProcessing>();



       
        return services;
    }

}