using System.Globalization;
using Company.Product.Domain.Contracts;
using Company.Product.Domain.DomainServices;
using Company.Product.Domain.Entities;
using Company.Product.Infrastructure.Repository.Persistence.SqlDb;
using Company.Product.Infrastructure.Services.OrderProcessing;
using Microsoft.Extensions.Configuration;

namespace Company.Product.Infrastructure.Tests.Integration.Services;

public class LoggedOrdersTests
{
    private  readonly ISqlDb _moqDb;
    private readonly IOrderAddAttributService _orderAddAttributService;
    
    public LoggedOrdersTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Der Pfad, in dem sich die appsettings.json befindet
            .AddJsonFile("appsettings_infrastructure_tests.json", optional: false, reloadOnChange: true); // Füge die Konfigurationsdatei hinzu
            var configuration = builder.Build(); // Erstelle die IConfiguration-Instanz
            _moqDb = new SqlDb(configuration.GetConnectionString("TestDB"));

            _orderAddAttributService = new OrderAddAttributService();
    }

   
    [Theory]
    [InlineData(  "2025-03-10 22:08:00", "2025-03-11 00:38:00")]
    public void FetchOrders_WhenValidDateTime_ShouldHaveResultsAndPass( string fromString, string toString)
    {
        
        // Arrange
         var from = DateTime.Parse(fromString, new CultureInfo("de-DE"));
         var to = DateTime.Parse(toString, new CultureInfo("de-DE"));
         var apiSource = new LoggedOrders(_moqDb, _orderAddAttributService);
         

        // Act
        var result = apiSource.RetrieveByDate(from, to);
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<RequestOrder>>(result);
    }
    
    
    [Theory]
    [InlineData(  "2023-01-", "2023-")]
    public void FetchOrders_WhenInvalidDateTime_HaveValidationError( string fromString, string toString)
    {
        // Arrange
        var apiSource = new LoggedOrders(_moqDb, _orderAddAttributService);
        
        // Act & Assert
        Assert.Throws<FormatException>(
            () =>
            {
                var from = DateTime.Parse(fromString, new CultureInfo("de-DE"));
                var to = DateTime.Parse(toString, new CultureInfo("de-DE"));
                apiSource.RetrieveByDate(from, to);
            }
        );
    }
    
    
    [Theory]
    [InlineData(  "2025-03-10 22:08:00", "2025-03-10 22:08:59")]
    public void FetchOrders_WhenValidDateTime_ShouldHaveOneResultAndPass( string fromString, string toString)
    {
        
        // Arrange
        var from = DateTime.Parse(fromString, new CultureInfo("de-DE"));
        var to = DateTime.Parse(toString, new CultureInfo("de-DE"));
        var apiSource = new LoggedOrders(_moqDb, _orderAddAttributService);
         
         
        // Act
        var result = apiSource.RetrieveByDate(from, to);
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<RequestOrder>>(result);
        Assert.Single(result);
    }
    
    
        
    [Theory]
    [InlineData(  "2025-03-10 22:08:00", "2025-03-10 22:08:00")]
    public void FetchOrders_WhenValidDateTime_ShouldbeEmptyAndPass( string fromString, string toString)
    {
        
        // Arrange
        var from = DateTime.Parse(fromString, new CultureInfo("de-DE"));
        var to = DateTime.Parse(toString, new CultureInfo("de-DE"));
        var apiSource = new LoggedOrders(_moqDb, _orderAddAttributService);
         
         
        // Act
        var result = apiSource.RetrieveByDate(from, to);
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<RequestOrder>>(result);
        Assert.Empty(result);
    }
}