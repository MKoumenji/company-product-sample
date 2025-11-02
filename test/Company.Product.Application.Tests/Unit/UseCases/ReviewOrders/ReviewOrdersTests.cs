using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Application.Tests.Unit.UseCases.Mock;
using Company.Product.Application.UseCases.Models;
using Company.Product.Domain.Contracts;
using Company.Product.Domain.DomainServices;
using Company.Product.Domain.Entities;
using Company.Product.Infrastructure.Services.Logging;
using FluentAssertions;

namespace Company.Product.Application.Tests.Unit.UseCases.ReviewOrders;

public class ReviewOrdersTests
{
    private readonly Application.UseCases.ReviewOrders _reviewOrders;

    public ReviewOrdersTests()
    {
        IOrderValidationService validationService = new OrderValidationService();
        ISecLoggerService loggerService = new SecLoggerService();
        _reviewOrders = new Application.UseCases.ReviewOrders(validationService, loggerService);
    }
    
    [Fact]
    public void Execute_WhenHasNewOrders_ShouldRPass()
    {
        // Arrange
        var loggedOrders = new List<RequestOrder>
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20"),
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };
        
        
        var inspectionResult = new InspectionResult
        {
            LoggedOrders = loggedOrders,
            ProcessedOrders = new List<int>(),
            PendingOrders = new List<RequestOrder>()
        };

        // Act
        var result = _reviewOrders.Execute(inspectionResult);

        // Assert
         result.Should().BeTrue();
    }
    
    
    [Fact]
    public void Execute_WhenAllOrdersImported_ShouldPass()
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
        
       
        
        var inspectionResult = new InspectionResult
        {
            LoggedOrders = loggedOrders,
            ProcessedOrders = processedOrders,
            PendingOrders = new List<RequestOrder>()
        };

        // Act
        var result = _reviewOrders.Execute(inspectionResult);

        // Assert
        result.Should().BeFalse();
        inspectionResult.ProcessedOrders.Count.Should().Be(4);
        
        var hashSet = new HashSet<int>(inspectionResult.ProcessedOrders);
        processedOrders.All(item => hashSet.Contains(item)).Should().BeTrue();
        
    }
    
    
    [Fact]
    public void Execute_WhenHasPendingOrders_ShouldPass()
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
            124
        };
        
        var pendingOrders = new List<RequestOrder>
        {
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };
        
        
        var inspectionResult = new InspectionResult
        {
            LoggedOrders = loggedOrders,
            ProcessedOrders = processedOrders,
            PendingOrders = new List<RequestOrder>()
        };

        // Act
       _reviewOrders.Execute(inspectionResult);

        // Assert
        inspectionResult.PendingOrders.Count.Should().Be(2);
        
        var hashSet = new HashSet<int>(inspectionResult.PendingOrders.Select(x=>x.OrderNumber));
        pendingOrders.All(item => hashSet.Contains(item.OrderNumber)).Should().BeTrue();
        
    }


}