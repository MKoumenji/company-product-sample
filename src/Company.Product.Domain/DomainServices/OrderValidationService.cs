using Company.Product.Domain.Contracts;
using Company.Product.Domain.Entities;

namespace Company.Product.Domain.DomainServices;

public class OrderValidationService:IOrderValidationService
{
    public bool HasNewOrders(List<RequestOrder> loggedOrders)
        => loggedOrders.Any();

    public bool HasPendingOrders(List<RequestOrder> loggedOrders, List<int> processedOrders)
    {
        // Find logged but not handled Orders
        var processedOrderHSet = new HashSet<int>(processedOrders);
        return loggedOrders
            .Where(logOrderId => 
                !processedOrderHSet.Contains(logOrderId.OrderNumber)).AsEnumerable().Any();
    }
}