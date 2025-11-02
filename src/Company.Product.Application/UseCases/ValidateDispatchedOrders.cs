using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Application.UseCases.Abstarction;
using Company.Product.Application.UseCases.Models;

namespace Company.Product.Application.UseCases;

public class ValidateDispatchedOrders (IRetrieveProcessedOrders retrieveProcessedOrders , ISecLoggerService secLoggerService
    , IOrderHandlerHistory orderHandlerHistory,  INotifierService notifierService) : IValidateDispatchedOrders
{
    public bool Execute(InspectionResult inspectionResult)
    {
        //Verify if pending orders were sucessfully Dispached?  
        secLoggerService.LogInformation("Check if pending were sucessfully processed");
        var dipatchedOrders = retrieveProcessedOrders.RetrieveById(inspectionResult.PendingOrders
            .Select(order => order.OrderNumber).ToList());
            
        if (dipatchedOrders.Any())
        {
            if (dipatchedOrders.Count == inspectionResult.PendingOrders.Count)
            {
                secLoggerService.LogInformation($"All pending Orders were Successfully Imported");
                notifierService.SendInformationEmail("company.product-Information",
                    $"All pending Orders were Successfully Imported: {string.Join(" | ", dipatchedOrders)}");
            }
            else
            {
                secLoggerService.LogWarning($"Not all pending Orders were Successfully Imported, this is a list of successfully imported Orders: {string.Join(" | ", dipatchedOrders)}");
                notifierService.SendWarningEmail("company.product-Warning",
                    $"Not all pending Orders were Successfully Imported, this is a list of successfully imported Orders: {string.Join(" | ", dipatchedOrders)}");
            }
            return true;
        }
        
        //Order runs on TimeOut?
        var unprocessedHistory = orderHandlerHistory.FetchUnprocessedOrders(inspectionResult.PendingOrders.Select(order=> order.OrderNumber).ToList());
        if (unprocessedHistory.Any())
        {
            secLoggerService.LogWarning($"Pending orders runs on TimeOut: {string.Join(" | ", unprocessedHistory.Select(order => order.OrderNumber))}");
            return true;
        }
        return false;
    }
       
    
}