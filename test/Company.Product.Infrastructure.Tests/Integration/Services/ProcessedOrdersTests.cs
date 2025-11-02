using Company.Product.Infrastructure.Repository.Persistence.SqlDb;
using Company.Product.Infrastructure.Services.OrderProcessing;
using Microsoft.Extensions.Configuration;

namespace Company.Product.Infrastructure.Tests.Integration.Services;

public class ProcessedOrdersTests
{
    private  readonly ISqlDb _moqTestDb;

    public ProcessedOrdersTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings_infrastructure_tests.json", optional: false, reloadOnChange: true); 
        IConfiguration configuration = builder.Build(); 
        _moqTestDb = new SqlDb(configuration.GetConnectionString("TestDB"));
    }
    
    [Theory]
    [InlineData(new[] { 12345, 1254898 })]
    public void FetchOrdersByOrderId_WhenNoValidIds_ShouldHaveNoResultsAndPass(int[] orderIds)
    {
        // Arrange
        var erpSource = new ProcessedOrders(_moqTestDb);
        var idList = orderIds.ToList();

        // Act
        var result = erpSource.RetrieveById(idList);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<int>>(result);
        Assert.Empty(result);
    }
    
    [Theory]
    [InlineData(new[] {1126006,1125997,1125989,112598999, 1125989777})]
    public void FetchOrdersByOrderId_WhenValidIds_ShouldHaveResultsAndPass(int[] orderIds)
    {
        // Arrange
        var erpSource = new ProcessedOrders(_moqTestDb);
        var idList = orderIds.ToList();

        // Act
        var result = erpSource.RetrieveById(idList);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<int>>(result);
        Assert.NotEmpty(result);
        Assert.Equal(orderIds.Length,result.Count);
    }
}