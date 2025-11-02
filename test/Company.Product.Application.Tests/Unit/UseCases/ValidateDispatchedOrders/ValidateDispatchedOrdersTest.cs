using Company.Product.Application.Tests.Unit.UseCases.Mock;
using Company.Product.Application.UseCases.Models;
using Company.Product.Domain.Entities;
using FluentAssertions;

namespace Company.Product.Application.Tests.Unit.UseCases.ValidateDispatchedOrders;

public class ValidateDispatchedOrdersTest
{
    private readonly RetrieveProcessedOrdersMock  _retrieveProcessedOrdersMock = new RetrieveProcessedOrdersMock();
    private readonly SecLoggerMock _logger = new ();
    private readonly OrderHandlerHistoryMock _orderHandlerHistoryMock = new ();
    private readonly NotifierServiceMock _notifierServiceMock = new ();
    private Application.UseCases.ValidateDispatchedOrders? _useCase;
    
    [Fact]
    public void Execute_WhenAllPendingOrdersProcessed_ShouldPass()
    {
        // Arrange
        var loggedOrdersInRange = new List<RequestOrder>()
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20"),
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };
        
        var processedOrdersInAsRequestOrder= new List<RequestOrder>()
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20"),
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };
        
        var processedOrders = new List<int>()
        {
            123,
            124,
            125,
            126
        };
        
        var inspectionResult = new InspectionResult
        {
            LoggedOrders = loggedOrdersInRange,
            ProcessedOrders = processedOrders,
            PendingOrders = processedOrdersInAsRequestOrder
        };
        
        _retrieveProcessedOrdersMock.AddOrders(processedOrdersInAsRequestOrder.ToArray());
        
        _useCase = new Application.UseCases.ValidateDispatchedOrders(_retrieveProcessedOrdersMock, 
            _logger, _orderHandlerHistoryMock, _notifierServiceMock);
        
        // Act
        var result = _useCase.Execute(inspectionResult);

        // Assert
        result.Should().BeTrue();
    }


    [Fact]
    public void Execute_WhenNoPendingOrdersProcessed_ShouldPass()
    {
        
        var loggedOrdersInRange = new List<RequestOrder>()
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20"),
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };
        
        var processedOrdersInAsRequestOrder= new List<RequestOrder>()
        {
            RequestOrderMock.CreateTestOrder(123, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(124, "2024-01-20"),
            RequestOrderMock.CreateTestOrder(125, "2024-01-15"),
            RequestOrderMock.CreateTestOrder(126, "2024-01-20")
        };
        
        
        var inspectionResult = new InspectionResult
        {
            LoggedOrders = loggedOrdersInRange,
            ProcessedOrders = new List<int>(),
            PendingOrders = new List<RequestOrder>()
        };
        
        _retrieveProcessedOrdersMock.AddOrders(processedOrdersInAsRequestOrder.ToArray());
        
        _useCase = new Application.UseCases.ValidateDispatchedOrders(_retrieveProcessedOrdersMock, 
            _logger, _orderHandlerHistoryMock, _notifierServiceMock);
        
        // Act
        var result = _useCase.Execute(inspectionResult);

        // Assert
        result.Should().BeFalse();
    }
}

