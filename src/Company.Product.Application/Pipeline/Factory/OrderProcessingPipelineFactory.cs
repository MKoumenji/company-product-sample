using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Pipeline.Models;
using Company.Product.Application.Pipeline.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Product.Application.Pipeline.Factory;

public class OrderProcessingPipelineFactory(IServiceProvider serviceProvider): IOrderProcessingPipelineFactory

{
    public Pipeline<KontextData> CreatePipeline()
    {
        var pipeline = new Pipeline<KontextData>();
        // Register operations in the correct order
        pipeline.Register(serviceProvider.GetRequiredService<ScheduleCheckOperation>());
        pipeline.Register(serviceProvider.GetRequiredService<RetrieveOrdersOperation>());
        pipeline.Register(serviceProvider.GetRequiredService<ReviewOrdersOperation>());
        pipeline.Register(serviceProvider.GetRequiredService<DispatchPendingOrdersOperation>());
        pipeline.Register(serviceProvider.GetRequiredService<ValidateDispatchedOrdersOperation>());
        pipeline.Register(serviceProvider.GetRequiredService<RestartOrderHandlerOperation>());
        pipeline.Register(serviceProvider.GetRequiredService<DispatchPendingOrdersOperation>()); // Retry dispatch
        
        return pipeline;
    }
}