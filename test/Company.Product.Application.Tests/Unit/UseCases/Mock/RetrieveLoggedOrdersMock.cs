using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Domain.Entities;

namespace Company.Product.Application.Tests.Unit.UseCases.Mock;

public class RetrieveLoggedOrdersMock: IRetrieveLoggedOrders

{
    private readonly List<RequestOrder> _orders = new();

    public void AddOrders(params RequestOrder[] orders)
    {
        _orders.AddRange(orders);
    }

    public List<RequestOrder> RetrieveByDate(DateTime from, DateTime to)
    {
        return _orders
            .Where(o => DateTime.Parse(o.OrderDate) >= from && DateTime.Parse(o.OrderDate) <= to)
            .ToList();
    }

    public void Clear()
    {
        _orders.Clear();
    }

}