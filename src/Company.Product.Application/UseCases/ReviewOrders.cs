using System.Collections.Generic;
using System.Linq;
using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Application.UseCases.Abstarction;
using Company.Product.Application.UseCases.Models;
using Company.Product.Domain.Contracts;

namespace Company.Product.Application.UseCases;

public class ReviewOrders(IOrderValidationService validationServiceManager,  ISecLoggerService secLoggerService) : IReviewOrders
{

    public bool Execute(InspectionResult inspectionResult)
    {
        //Check for new Orders
        if (!HasNewOrders(inspectionResult))
        {
            secLoggerService.LogInformation($"No new orders were found");
            return true;
        }

        //Were all Orders imported?
        if (!HasPendingOrders(inspectionResult))
        {
            secLoggerService.LogInformation("All Orders were Successfully processed");
            return true;
        }

        //Has Pending orders?
        if (HasPendingOrders(inspectionResult))
        {
            // Finde IDs, die im Log vorhanden sind, aber nicht im Orderhandler
            GetPendingOrders(inspectionResult);
            secLoggerService.LogInformation(
                $"Pending Orders {string.Join(",", inspectionResult.PendingOrders.Select(id => id.OrderNumber))}");
        }
        return false;
    }
    
    private bool HasNewOrders(InspectionResult inspectionResult)
        => validationServiceManager.HasNewOrders(inspectionResult.LoggedOrders);

    private bool HasPendingOrders(InspectionResult inspectionResult)
        => validationServiceManager.HasPendingOrders(inspectionResult.LoggedOrders, inspectionResult.ProcessedOrders);


    private static void GetPendingOrders(InspectionResult inspectionResult )
    {
        var processedOrderHSet = new HashSet<int>(inspectionResult.ProcessedOrders);
        inspectionResult.PendingOrders =  inspectionResult.LoggedOrders
            .Where(logOrderId => 
                !processedOrderHSet.Contains(logOrderId.OrderNumber)).ToList();
    }
}