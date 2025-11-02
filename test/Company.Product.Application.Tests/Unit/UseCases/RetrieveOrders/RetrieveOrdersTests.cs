using Company.Product.Application.Tests.Unit.UseCases.Mock;
using FluentAssertions;

namespace Company.Product.Application.Tests.Unit.UseCases.RetrieveOrders;

public class RetrieveOrdersTests
{
    private readonly RetrieveLoggedOrdersMock _retrieveLoggedOrdersMock;
    private readonly RetrieveProcessedOrdersMock _retrieveProcessedOrdersMock;
    private readonly Application.UseCases.RetrieveOrders _useCase;

    public RetrieveOrdersTests()
    {
        _retrieveLoggedOrdersMock = new RetrieveLoggedOrdersMock();
        _retrieveProcessedOrdersMock = new RetrieveProcessedOrdersMock();
        var logger = new SecLoggerMock();
        _useCase = new Application.UseCases.RetrieveOrders(_retrieveLoggedOrdersMock, _retrieveProcessedOrdersMock, logger);
    }
    
    

 [Fact]
    public void Execute_WhenLoggedOrderFoundWithinDateRange_ShouldReturnLoggedOrders()
    {
        // Arrange
        var fromDate = new DateTime(2024, 1, 1);
        var toDate = new DateTime(2024, 1, 31);
        
        var ordersInRange = new[]
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20")
        };
        
        var ordersOutOfRange = new[]
        {
            RequestOrderMock.CreateTestOrder(125, "2023-12-31"), // Before range
            RequestOrderMock.CreateTestOrder(126, "2024-02-01")  // After range
        };

        _retrieveLoggedOrdersMock.AddOrders(ordersInRange.Concat(ordersOutOfRange).ToArray());

        // Act
        var result = _useCase.Execute(fromDate, toDate);

        // Assert
        result.LoggedOrders.Should().HaveCount(2);
        result.LoggedOrders.Select(x => x)
            .Where(x => x.OrderNumber == 123 || x.OrderNumber == 124).Should().HaveCount(2);
    }

    [Fact]
    public void Execute_WhenNoLoggedOrdersFoundWithInTheRange_ShouldReturnEmptyResult()
    {
        // Arrange
        var fromDate = new DateTime(2024, 1, 1);
        var toDate = new DateTime(2024, 1, 31);
   
        // Act
        var result = _useCase.Execute(fromDate, toDate);

        // Assert
        result.LoggedOrders.Should().BeEmpty();
    }


    
    [Fact]
    public void Execute_WhenProcessedOrderFoundWithinDateRange_ShouldPass()
    {
        // Arrange
        var fromDate = new DateTime(2024, 1, 1);
        var toDate = new DateTime(2024, 1, 31);
        
        var loggedOrdersInRange = new[]
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20"),
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };
        
        var processedOrdersInRange = new[]
        {
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"), 
            RequestOrderMock.CreateTestOrder(126, "2024-01-20") 
        };

         _retrieveLoggedOrdersMock.AddOrders(loggedOrdersInRange.ToArray());
        _retrieveProcessedOrdersMock.AddOrders(processedOrdersInRange.ToArray());

        // Act
        var result = _useCase.Execute(fromDate, toDate);

        // Assert
        result.LoggedOrders.Should().HaveCount(4);
        result.ProcessedOrders.Select(x => x)
            .Where(x => x == 125 || x == 126).Should().HaveCount(2);
    }
    
    
    [Fact]
    public void Execute_WhenNoProcessedOrderFoundWithinDateRange_ShouldRPass()
    {
        // Arrange
        var fromDate = new DateTime(2024, 1, 1);
        var toDate = new DateTime(2024, 1, 31);
        
        var loggedOrdersInRange = new[]
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20"),
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };

        _retrieveLoggedOrdersMock.AddOrders(loggedOrdersInRange.ToArray());
        
        // Act
        var result = _useCase.Execute(fromDate, toDate);

        // Assert
        result.LoggedOrders.Should().HaveCount(4);
        result.ProcessedOrders.Should().BeEmpty();
    }
}
    