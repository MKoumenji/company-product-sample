using Company.Product.Domain.Contracts;
using Company.Product.Domain.Entities;

namespace Company.Product.Domain.DomainServices;

public class OrderAddAttributService:IOrderAddAttributService
{
    
    public RequestOrder AddShopAndSubShop(RequestOrder order)
    {
        switch (order.DeliveryAddress.CountryNumeric)
        {
            case 276:
                order.Filiale = 800;
                order.Subshop = 115;
                break;
            case 40:
                order.Filiale = 801;
                order.Subshop = 120;
                break;
            case 203:
                order.Filiale = 802;
                order.Subshop = 130;
                break;
            default:
                order.Filiale = 800;
                order.Subshop = 135;
                break;
        }

        return order;
    }
}