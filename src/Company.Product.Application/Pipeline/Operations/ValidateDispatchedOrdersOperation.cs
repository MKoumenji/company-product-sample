using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Pipeline.Models;
using Company.Product.Application.UseCases.Abstarction;

namespace Company.Product.Application.Pipeline.Operations;

public class ValidateDispatchedOrdersOperation(IValidateDispatchedOrders
    validateOrders) 
    : IOperation<KontextData>

{
    public void Invoke(KontextData results)
    {
        results.ExecutedOperations.Add(nameof(ValidateDispatchedOrdersOperation));
        
        if (!results.ShouldContinue || results.CancellationToken.IsCancellationRequested)
            return;

        try
        {
            if (results.InspectionResult != null && validateOrders.Execute(results.InspectionResult))
            {
                // Validation successful, all orders processed
                results.ShouldContinue = false;
            }
        }
        catch (Exception ex)
        {
            results.Exception= ex;
            results.ExceptionOccurred = true;
            results.ShouldContinue = false;
        }

    }
}