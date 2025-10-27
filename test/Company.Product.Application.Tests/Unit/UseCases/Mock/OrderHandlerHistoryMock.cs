using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Domain.Entities;

namespace Company.Product.Application.Tests.Unit.UseCases.Mock;

public class OrderHandlerHistoryMock:IOrderHandlerHistory
{
    public List<RequestOrder> FetchUnprocessedOrders(List<int> orderIds)
    {
        List<RequestOrder> orders = new();
        foreach (var order in orderIds)
        {
            orders.Add(RequestOrderMock.CreateTestOrder(order, "2024-01-15")); 
        }

        return orders;
    }

    public void DeleteUnprocessedOrdersByOrderId(List<int> orderIds)
    {
    }
}