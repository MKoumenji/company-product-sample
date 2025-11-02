using Company.Product.Domain.Entities;
using Company.Product.Domain.ValueObjects;

namespace Company.Product.Application.Tests.Unit.UseCases.Mock;

public class RequestOrderMock
{
    public static RequestOrder CreateTestOrder(int orderNumber, string orderDate)
    {
        return new RequestOrder
        {
            OrderNumber = orderNumber,
            OrderDate = orderDate,
            Currency = "EUR",
            Customer = new Customer
            {
                CustomerNumber = 12345,
                Firstname = "Test",
                Lastname = "Customer",
                Email = "test@example.com"
            },
            InvoiceAddress = new InvoiceAddress
            {
                Street = "Test Street",
                HouseNumber = "1",
                Zip = "12345",
                City = "Test City",
                CountryName = "Germany",
                CountryNumeric = 276
            },
            DeliveryAddress = new DeliveryAddress
            {
                Street = "Test Street",
                Zip = "12345",
                City = "Test City",
                CountryName = "Germany",
                CountryNumeric = 276
            },
            ShoppingCart = new List<ShoppingCartItem>(),
            ShippingType = new ShippingType
            {
                Id = "545454",
                Description = "Standard",
                Tax = ""
            },
            ShippingData = new ShippingData
            {
                KepNumber = "",
                KepEmail = "",
                Wishtime = ""
            },
            PaymentTypeDetail = "Credit Card",
            ScoreDate = orderDate,
            ScoreText = "Good"
        };
    }
}