using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Pipeline.Models;
using Company.Product.Application.UseCases.Abstarction;

namespace Company.Product.Application.Pipeline.Operations;

public class ReviewOrdersOperation(IReviewOrders reviewOrders) : IOperation<KontextData>
    
{
    public void Invoke(KontextData results)
    {
        results.ExecutedOperations.Add(nameof(ReviewOrdersOperation));
        
        if (!results.ShouldContinue || results.CancellationToken.IsCancellationRequested)
            return;

        try
        {
            if (results.InspectionResult != null && reviewOrders.Execute(results.InspectionResult))
                  results.ShouldContinue = false;  // All orders were successfully processed, stop pipeline
        }
        catch (Exception ex)
        {
            results.Exception= ex;
            results.ExceptionOccurred = true;
            results.ShouldContinue = false;
        }

    }
}