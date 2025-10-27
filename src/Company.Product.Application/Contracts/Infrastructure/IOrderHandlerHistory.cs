using System.Collections.Generic;
using Company.Product.Domain.Entities;

namespace Company.Product.Application.Contracts.Infrastructure;

public interface IOrderHandlerHistory
{
    public List<RequestOrder> FetchUnprocessedOrders(List<int> orderIds);
    public void DeleteUnprocessedOrdersByOrderId(List<int> orderIds);
}