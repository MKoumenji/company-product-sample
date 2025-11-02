using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Application.UseCases.Abstarction;
using Company.Product.Application.UseCases.Models;

namespace Company.Product.Application.UseCases;

public class DispatchPendingOrders(IOrderHandler orderHandler,  ISecLoggerService secLoggerService, IOrderHandlerHistory orderHandlerHistory) : IDispatchPendingOrders
{

    private const int SecToMilliseconds = 1000;
    public void Execute(InspectionResult inspectionResult, string url, int pocessingTime)
    {
        //Delete pending orders from OrderHistory
        DeletePendingOrdersFromOrderHistory(inspectionResult);
        
        //Dispatch Pending orders
        secLoggerService.LogInformation("Dispatch pending Orders");
       
        foreach (var order in  inspectionResult.PendingOrders)
             orderHandler.Send(order,  url+$"?filiale={order.Filiale}&subshop={order.Subshop}");
        
        //wait
        secLoggerService.LogInformation($"Wait for processing pending Orders {inspectionResult.PendingOrders.Count * pocessingTime} Seconds");
        Thread.Sleep(inspectionResult.PendingOrders.Count * pocessingTime * SecToMilliseconds);
    }
    
    private void DeletePendingOrdersFromOrderHistory(InspectionResult inspectionResult)
    {
        //Check pending Orders in OrderHistory  
        var pendingInOrdersHistory = orderHandlerHistory.FetchUnprocessedOrders(inspectionResult.PendingOrders
            .Select(order => order.OrderNumber).ToList());

        //If pending Orders in OrderHistory then delete Them 
        if (pendingInOrdersHistory.Count > 0)
        {
            secLoggerService.LogInformation("Deleting pending Orders in OrderHistory");
            orderHandlerHistory.DeleteUnprocessedOrdersByOrderId(pendingInOrdersHistory.Select(order => order.OrderNumber).ToList());
        }
    }
}