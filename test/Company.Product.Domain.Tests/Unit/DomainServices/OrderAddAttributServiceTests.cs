using Company.Product.Application.Tests.Unit.UseCases;
using Company.Product.Application.Tests.Unit.UseCases.Mock;
using Company.Product.Domain.DomainServices;
using FluentAssertions;

namespace Company.Product.Domain.Tests.Unit.DomainServices;

public class OrderAddAttributServiceTests
{
    private readonly OrderAddAttributService _addAttributService = new();
    
    [Fact]
    public void AddShopAndSubShop_WhenAddCorrectAttributes_ShouldPass()
    {
        // Arrange
        var orderGermany = RequestOrderMock.CreateTestOrder(123, "2024-01-15");
        orderGermany.DeliveryAddress.CountryNumeric = 276;
        var orderAustria = RequestOrderMock.CreateTestOrder(124, "2024-01-15");
        orderAustria.DeliveryAddress.CountryNumeric = 40;
        var orderSwitzerland = RequestOrderMock.CreateTestOrder(125, "2024-01-15");
        orderSwitzerland.DeliveryAddress.CountryNumeric = 203;
        var defaultOrderOther = RequestOrderMock.CreateTestOrder(126, "2024-01-15");
        defaultOrderOther.DeliveryAddress.CountryNumeric = 999;


        // Act
        var resultGermany = _addAttributService.AddShopAndSubShop(orderGermany);
        var resultAustria = _addAttributService.AddShopAndSubShop(orderAustria);
        var resultSwitzerland = _addAttributService.AddShopAndSubShop(orderSwitzerland);
        var resultOther = _addAttributService.AddShopAndSubShop(defaultOrderOther);

        // Assert
        resultGermany.Filiale.Should().Be(800);
        resultGermany.Subshop.Should().Be(115);

        resultAustria.Filiale.Should().Be(801);
        resultAustria.Subshop.Should().Be(120);

        resultSwitzerland.Filiale.Should().Be(802);
        resultSwitzerland.Subshop.Should().Be(130);

        resultOther.Filiale.Should().Be(800);
        resultOther.Subshop.Should().Be(135);
    }
}