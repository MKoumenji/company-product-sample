using System.Net;
using Company.Product.Infrastructure.Repository.ExternalServices.RestClient;
using Company.Product.Infrastructure.Repository.WebIntegration.WebAppManager;
using Company.Product.Infrastructure.Services.OrderProcessing;
using Company.Product.Infrastructure.Tests.Integration.Services.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Company.Product.Infrastructure.Tests.Integration.Services;

public class OrderHandlerTests
{
    
    private  IWebAppManagerIis _webAppManagerIis = null!;
    private readonly IConfiguration _configuration;
    private readonly IRestClient _restClient;
    private readonly string _endpoint;


    public OrderHandlerTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings_infrastructure_tests.json", optional: false, reloadOnChange: true);
         _configuration = builder.Build();
         
        _endpoint =  _configuration["OrchestratorSettings:OrderHandlerUrl"]!;
        _restClient = new RestClient(new HttpClient());

       
    }
    
    [Fact]
    public void OrderHandler_StopWebApp_WhenValidConfig_ShouldPass()
    {
        // Arrange
        var services = new ServiceCollection();
        services.Configure<IisConfig>(_configuration.GetSection("IisConfig"));
        var serviceProvider = services.BuildServiceProvider();
        
        // Hole die konfigurierten Optionen
        var options = serviceProvider.GetRequiredService<IOptions<IisConfig>>();
        
        _webAppManagerIis = new WebAppManagerIis(options);
        var orderHandler = new OrderHandler(_webAppManagerIis, _restClient);
        
        // Act
        var result =  orderHandler.Stop();

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void OrderHandler_StartWebApp_WhenValidConfig_ShouldPass()
    {
        // Arrange
        var services = new ServiceCollection();
        services.Configure<IisConfig>(_configuration.GetSection("IisConfig"));
        var serviceProvider = services.BuildServiceProvider();
        
        // Hole die konfigurierten Optionen
        var options = serviceProvider.GetRequiredService<IOptions<IisConfig>>();
        
        _webAppManagerIis = new WebAppManagerIis(options);
        var orderHandler = new OrderHandler(_webAppManagerIis, _restClient);
        
        // Act
       var result =  orderHandler.Start();

        // Assert
        Assert.True(result);
    }
    
    
   
    [Fact]
    public void StartWebApp_WhenInvaliValidWebAppName_ShouldThrowException()
    {

        // Arrange
        var services = new ServiceCollection();
        services.Configure<IisConfig>(_configuration.GetSection("IisConfig"));
        var serviceProvider = services.BuildServiceProvider();
        
        // Hole die konfigurierten Optionen
        var options = serviceProvider.GetRequiredService<IOptions<IisConfig>>();
        options.Value.SiteName = "InvalidSiteName";
        _webAppManagerIis = new WebAppManagerIis(options);
        
        var webAppManager = new WebAppManagerIis(options);
      

        // Act & Assert
        Assert.Throws<NullReferenceException>(
            () => webAppManager.StartWebApp()); 
    }
    
    [Fact]
    public void StopWebApp_WhenInvaliValidWebAppName_ShouldThrowException()
    {
        // Arrange
        var services = new ServiceCollection();
        services.Configure<IisConfig>(_configuration.GetSection("IisConfig"));
        var serviceProvider = services.BuildServiceProvider();
        
        // Hole die konfigurierten Optionen
        var options = serviceProvider.GetRequiredService<IOptions<IisConfig>>();
        options.Value.SiteName = "InvalidSiteName";
        _webAppManagerIis = new WebAppManagerIis(options);
        
        var webAppManager = new WebAppManagerIis(options);
      

        // Act
        Assert.Throws<NullReferenceException>(() => webAppManager.StopWebApp()); 
        
    }

    [Fact]
    public async Task OrderHandler_Send_WhenValidConfig_ShouldPass()
    {
        // Arrange
        var orderHandler = new OrderHandler(_webAppManagerIis, _restClient);
        
        // Act
        var order = RequestOrderGenerator.CreateMockOrder();
        var result =  await orderHandler.Send( order,_endpoint+"?filiale=800&subshop=115");
        
        // Read the HTTP status code
        HttpStatusCode statusCode = result.StatusCode;
        
        //Assert
        Assert.True(result.IsSuccessStatusCode);
    }
    
    
    [Fact]
    public  void OrderHandler_Send_InvalidEndpoint_ShouldFail()
    {
        // Arrange
        var orderHandler = new OrderHandler(_webAppManagerIis, _restClient);
       
        // Act & Assert
         Assert.ThrowsAsync<InvalidOperationException>(async ()=>
        {
             await orderHandler.Send(RequestOrderGenerator.CreateMockOrder(), "invalid-endpoint");
        });
        
    }

}