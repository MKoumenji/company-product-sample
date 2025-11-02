using Company.Product.Application.DTOs.Config;
using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Pipeline.Models;
using Company.Product.Application.UseCases.Abstarction;
using Microsoft.Extensions.Options;

namespace Company.Product.Application.Pipeline.Operations;

public class DispatchPendingOrdersOperation(IDispatchPendingOrders dispatchOrders, IOptions<ApplicationSettings> options) 
    : IOperation<KontextData>

{
    public void Invoke(KontextData results)
    {
        results.ExecutedOperations.Add(nameof(DispatchPendingOrdersOperation));
        
        if (!results.ShouldContinue || results.CancellationToken.IsCancellationRequested)
            return;

        try
        {
            if (results.InspectionResult != null)
                dispatchOrders.Execute(
                    results.InspectionResult,
                    options.Value.OrderHandlerUrl,
                    options.Value.OrderProcessingTimeInSec
                );
        }
        catch (Exception ex)
        {
            results.Exception= ex;
            results.ExceptionOccurred = true;
            results.ShouldContinue = false;
        }

    }
}