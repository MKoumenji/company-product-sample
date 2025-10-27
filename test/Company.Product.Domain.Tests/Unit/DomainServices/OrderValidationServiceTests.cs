
using Company.Product.Application.Tests.Unit.UseCases.Mock;
using Company.Product.Domain.DomainServices;
using Company.Product.Domain.Entities;
using FluentAssertions;

namespace Company.Product.Domain.Tests.Unit.DomainServices;

public class OrderValidationServiceTests
{
    private readonly OrderValidationService _validationService = new();

    [Fact]
    public void HasNewOrders_WhenNewOrdersExist_ShouldPass()
    {
        // Arrange
        var loggedOrders = new List<RequestOrder>
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20"),
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };

        // Act
        var result = _validationService.HasNewOrders(loggedOrders);

        // Assert
        result.Should().BeTrue();
    }
    
    
    [Fact]
    public void HasNewOrders_WhenNoNewOrdersExist_ShouldRPass()
    {
        // Arrange
        var loggedOrders = new List<RequestOrder>();

        // Act
        var result = _validationService.HasNewOrders(loggedOrders);

        // Assert
        result.Should().BeFalse();
    }
    
    
    [Fact]
    public void HasPendingOrders_WhenPendingOrdersExist_ShouldPass()
    {
        // Arrange
        var loggedOrders = new List<RequestOrder>
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20"),
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };
        
        var processedOrders = new List<int>
        {
            125, 
            126
        };
        // Act
        var result = _validationService.HasPendingOrders(loggedOrders,processedOrders);

        // Assert
        result.Should().BeTrue();
        
    }
    
    
    [Fact]
    public void HasNewOrders_WhenNoPendingOrdersExist_ShouldPass()
    {
        // Arrange
        var loggedOrders = new List<RequestOrder>
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20"),
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };
        
        var processedOrders = new List<int>
        {
            123,
            124,
            125, 
            126
        };
        // Act
        var result = _validationService.HasPendingOrders(loggedOrders,processedOrders);

        // Assert
        result.Should().BeFalse();
    }
}