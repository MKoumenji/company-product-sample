using System.Data;
using Company.Product.Application.Contracts.Presentation.Commands;
using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Services;
using Company.Product.Domain.Entities;
using Company.Product.Domain.ValueObjects;
using Company.Product.Infrastructure;
using Company.Product.Infrastructure.Repository.Persistence.SqlDb;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;

namespace Company.Product.Application.Tests.Integration.Services;

public class ProcessWorkflowHandlerTests
{
    private readonly ProcessWorkflowHandler _handler;
    private readonly ISqlDb _sqlDb;
    
    public ProcessWorkflowHandlerTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings_application_tests.json", optional: false, reloadOnChange: true);
        var configuration = builder.Build();
        
        _sqlDb= new SqlDb(configuration.GetConnectionString("TestDB"));
        
        var services = new ServiceCollection();
        services.AddApplicationLayer(configuration);
        services.AddinfrastructureLayer(configuration);
        
        var provider = services.BuildServiceProvider();
        

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration) // Lies Konfiguration aus appsettings
            .Enrich.WithExceptionDetails(
                new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
            )
            .Enrich.WithAssemblyName()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .Enrich.WithEnvironmentName()
            .CreateLogger();
        
        _handler = new ProcessWorkflowHandler(provider.GetRequiredService<IOperationProcessing>());
    } 
    

    [Fact]
    public async Task HandleUseCases_ImportUnprocessedOrders_ShouldExecuteSuccessfully()
    {
        // Arrange
        var orderIds =  InsertOrdersInRocketApiTransferObjectsRaw(5,out var logId); // erstellt orders

        // Act
        await _handler.Handle(new ProcessWorkflowCommand(), CancellationToken.None);

        await Task.Delay (2 * 1000);// Wait for the orders to be processed
        
        // Assert
        Assert.True(CheckIfPendingsOrdersWereInserted(orderIds));
        DeleteOrdersFromRocketApiTransferObjectsRaw(logId);
    }
    
    
    
    
    private void DeleteOrdersFromRocketApiTransferObjectsRaw( int logId)
    {
        var sql = $"DELETE FROM  FUTURERS.dbo.rocketapi_TransferObjectsRaw  WHERE LogId = {logId}";
        _sqlDb.Delete<int>(sql, null, CommandType.Text);
        
    }
    private bool CheckIfPendingsOrdersWereInserted(List<int> orderIds)
    {
        var orderIdsString = string.Join(",", orderIds);
        var sql = $"SELECT COUNT(OrderNumber) FROM rocketapi_CustomerOrderHead WHERE OrderNumber in ({orderIdsString})";
        var  count = _sqlDb.GetAll<int>(sql, null, CommandType.Text).AsEnumerable().FirstOrDefault();

        return count == orderIds.Count;
    }
    private List<int> InsertOrdersInRocketApiTransferObjectsRaw(int numberOfOrders, out int logId )
    {
        var orders = CreateOrders(numberOfOrders);
        var orderIds = new List<int>();
        string sql = $"SELECT max(LogId) FROM FUTURERS.dbo.rocketapi_TransferObjectsRaw";
        logId = _sqlDb.GetAll<int>(sql, null, CommandType.Text).AsList().FirstOrDefault()+1;

        foreach (var order in orders)
        {
            sql = "Insert INTO FUTURERS.dbo.rocketapi_TransferObjectsRaw (LogPath,LogBody,LogId, LogDateTime) " +
                  $"VALUES ('/api/v1/CustomerOrder/CustomerOrder', '{JsonConvert.SerializeObject(order)}', {logId}, DATEADD(MINUTE, -30, GETDATE()))";
           _sqlDb.Insert<int>(sql, null, CommandType.Text);
           orderIds.Add(order.OrderNumber);
        }
        return orderIds;
    }
    private List<RequestOrder> CreateOrders(int numberOfOrders)
    {
        //get Order
        string sql = $"SELECT LogBody FROM FUTURERS.dbo.rocketapi_TransferObjectsRaw WHERE LogId = (SELECT MAX(LogId) from FUTURERS.dbo.rocketapi_TransferObjectsRaw )";
        string? orderexample = _sqlDb.GetAll<string>(sql, null, CommandType.Text).AsList().FirstOrDefault();
        var order = JObject.Parse(orderexample ?? string.Empty).ToObject<RequestOrder>();
        if (order != null)
        {
            order.Customer.CustomerNumber = 991102689;
            order.Customer.Firstname = "Thomas";
            order.Customer.Lastname = "Mustermann";
            order.DeliveryAddress = new DeliveryAddress
            {
                Street = "Heestweg 7",
                Zip = "22143",
                City = "Hamburg",
                CountryNumeric = 276, // Assuming this is a valid country code
                CountryName = "Deutschland"
            };

            order.InvoiceAddress = new InvoiceAddress
            {
                Street = "Heestweg 7",
                Zip = "22143",
                City = "Hamburg",
                CountryNumeric = 276, // Assuming this is a valid country code
                CountryName = "Deutschland",
                HouseNumber = "12"
            };

            List<RequestOrder> retOrders = new List<RequestOrder>();
            for (int i = 0; i < numberOfOrders; i++)
            {
                var serialized = JsonConvert.SerializeObject(order);
                var orderCopy = JsonConvert.DeserializeObject<RequestOrder>(serialized);
                if (orderCopy != null)
                {
                    orderCopy.OrderNumber =
                        _sqlDb.GetAll<int>("FUTURERS.dbo.rocketapi_GetNextCustomerOrderNumber", null)
                            .AsEnumerable().FirstOrDefault();
                    retOrders.Add(orderCopy);
                }
            }

            return retOrders; // Placeholder return value
        }

        return new List<RequestOrder>();
    }
}