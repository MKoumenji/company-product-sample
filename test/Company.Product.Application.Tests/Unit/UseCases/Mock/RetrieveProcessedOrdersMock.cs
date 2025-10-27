using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Domain.Entities;

namespace Company.Product.Application.Tests.Unit.UseCases.Mock;

public class RetrieveProcessedOrdersMock:IRetrieveProcessedOrders
{
    private readonly List<RequestOrder> _ProcessedOrders = new();

    public void AddOrders(params RequestOrder[] orders)
    {
        _ProcessedOrders.AddRange(orders);
    }
    
    public List<int> RetrieveById(List<int> orderIds)
    { 
        return _ProcessedOrders.Select(x => x.OrderNumber)
            .Where( orderIds.Contains)
            .ToList();
    }

    public void Clear()
    {
        _ProcessedOrders.Clear();
    }

  
}