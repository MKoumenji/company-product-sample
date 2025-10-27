using Company.Product.Domain.Entities;

namespace Company.Product.Domain.Contracts;

public interface IOrderAddAttributService
{
    public RequestOrder AddShopAndSubShop(RequestOrder order);
}