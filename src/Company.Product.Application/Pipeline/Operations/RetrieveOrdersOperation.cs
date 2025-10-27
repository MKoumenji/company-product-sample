using Company.Product.Application.DTOs.Config;
using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Pipeline.Models;
using Company.Product.Application.UseCases.Abstarction;
using Microsoft.Extensions.Options;

namespace Company.Product.Application.Pipeline.Operations;

public class RetrieveOrdersOperation(IRetrieveOrders retrieveOrders, IOptions<ApplicationSettings> options) 
    : IOperation<KontextData>

{
    public void Invoke(KontextData results)
    {
        results.ExecutedOperations.Add(nameof(RetrieveOrdersOperation));
        
        if (!results.ShouldContinue || results.CancellationToken.IsCancellationRequested)
            return;

        try
        {
            var retrievedOrders = retrieveOrders.Execute(
                DateTime.Now.AddMinutes(-(options.Value.TimeSpanInMinutes + options.Value.MarginInMinutes)), 
                DateTime.Now.AddMinutes(-options.Value.MarginInMinutes)
            );
            
            results.InspectionResult = retrievedOrders;
        }
        catch (Exception ex)
        {
            results.Exception= ex;
            results.ExceptionOccurred = true;
            results.ShouldContinue = false;
        }

    }
}