using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Pipeline.Models;

namespace Company.Product.Application.Pipeline;

public class OperationProcessing(
    IOrderProcessingPipelineFactory pipelineFactory,
    ISecLoggerService logger,
    INotifierService notifier
): IOperationProcessing

{
    public async Task<Task> ExecuteAsync(CancellationToken cancellationToken)
    {
        var processingData = new KontextData { CancellationToken = cancellationToken };
        var pipeline = pipelineFactory.CreatePipeline();
        
        logger.LogInformation("Starting operaion processing pipeline");
        pipeline.Invoke(processingData);
        
        logger.LogInformation($"Pipeline completed successfully. Executed operations: {string.Join(", ", processingData.ExecutedOperations)}");
        await Task.Delay(processingData.SleepTimeInMSec, cancellationToken);
        
        
        if (processingData.ExceptionOccurred)
        {
            logger.LogError("Pipeline execution failed", processingData.Exception);
            await notifier.SendErrorEmail("Pipeline Exception", processingData.Exception.Message, processingData.Exception.StackTrace ?? "");
            return Task.FromException(processingData.Exception);
        }
        
        return Task.CompletedTask;

    }
}