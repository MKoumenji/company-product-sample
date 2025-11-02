using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Infrastructure.Repository.Persistence.SqlDb;
using Company.Product.Infrastructure.Services.Logging;
using Microsoft.Extensions.Configuration;

namespace Company.Product.Infrastructure.Tests.Integration.Services;

public class DbLoggerTests
{
    private readonly ISqlDb _moq1db;

    public DbLoggerTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings_infrastructure_tests.json", optional: false, reloadOnChange: true);
        var configuration = builder.Build();
        _moq1db = new SqlDb(configuration.GetConnectionString("TestDB"));
     
    }

    [Theory]
    [InlineData("TetOrder_1",1,true)]
    [InlineData("TetOrder_2",2,false)]
    public void LogSql_WhenValidOrder_ShouldPass( string order, int orderNumber, bool isProcessed)
    {
        //Arrange
        IDbLoggerService dbLoggerService = new DbLogger(_moq1db);
        
        //Act
        var ret = dbLoggerService.LogToDb(order, orderNumber, isProcessed);
        
        //Assert
        Assert.True(ret);
    }
    
}