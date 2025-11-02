using System;
using System.Collections.Generic;
using Company.Product.Domain.Entities;

namespace Company.Product.Application.Contracts.Infrastructure;

public interface IRetrieveLoggedOrders
{
    List<RequestOrder> RetrieveByDate(DateTime from, DateTime to);
}