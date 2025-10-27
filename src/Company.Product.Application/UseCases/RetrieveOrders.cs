using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Application.UseCases.Abstarction;
using Company.Product.Application.UseCases.Models;
using Company.Product.Domain.Entities;

namespace Company.Product.Application.UseCases;

public class RetrieveOrders(IRetrieveLoggedOrders retrieveLoggedOrderOrders, IRetrieveProcessedOrders retrieveProcessedOrders, ISecLoggerService secLoggerService) : IRetrieveOrders
{
    public InspectionResult Execute(DateTime from, DateTime to)
    {
        
        secLoggerService.LogInformation($"--------------------------------------------------------------------------------------------------------------------");
        secLoggerService.LogInformation($"Retrieve Orders between {from} and  {to}");
        
         List<RequestOrder> loggedOrders  = retrieveLoggedOrderOrders.RetrieveByDate(from, to);
         
        if (loggedOrders.Count == 0)
        {
             return new InspectionResult
             {
                 ProcessedOrders = new List<int>(),
                 LoggedOrders = new List<RequestOrder>(),
                 PendingOrders = new List<RequestOrder>()
             };// Exit gracefully if no Orders Logged
        }
        
        //Ignore orders with more than 30 items
        loggedOrders.RemoveAll(order => order.ShoppingCart?.Count > 30);
         List<int> processedOrders = retrieveProcessedOrders.RetrieveById(loggedOrders.Select(order => order.OrderNumber).ToList());
         
        return new InspectionResult
        {
            ProcessedOrders = processedOrders,
            LoggedOrders = loggedOrders,
            PendingOrders = new List<RequestOrder>()
        };
    }
    
    
    
}