
using System.Collections.Generic;
using Company.Product.Domain.Entities;

namespace Company.Product.Application.Contracts.Infrastructure;

public interface IRetrieveProcessedOrders
{
    List<int> RetrieveById(List<int> orderIds);
}