using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Pipeline.Models;
using Company.Product.Application.UseCases.Abstarction;

namespace Company.Product.Application.Pipeline.Operations;

public class RestartOrderHandlerOperation(IRestartOrderHandler restartHandler) 
    : IOperation<KontextData>

{
    public void Invoke(KontextData results)
    {
        results.ExecutedOperations.Add(nameof(RestartOrderHandlerOperation));
        
        if (!results.ShouldContinue || results.CancellationToken.IsCancellationRequested)
            return;

        try
        {
            restartHandler.Execute();
        }
        catch (Exception ex)
        {
            results.Exception= ex;
            results.ExceptionOccurred = true;
            results.ShouldContinue = false;
        }

    }
}