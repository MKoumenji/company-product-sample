using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Domain.Entities;
using Company.Product.Infrastructure.Repository.ExternalServices.RestClient;
using Company.Product.Infrastructure.Repository.WebIntegration.WebAppManager;
using Newtonsoft.Json;

namespace Company.Product.Infrastructure.Services.OrderProcessing;

public class OrderHandler(IWebAppManagerIis webAppManagerIis, IRestClient restClient) : IOrderHandler
{
    public bool Start() 
        => webAppManagerIis.StartWebApp();
    
    public bool Stop()
        => webAppManagerIis.StopWebApp();
    
    public async Task<HttpResponseMessage> Send(RequestOrder order, string url)
        =>  await restClient.PostAsync(SerializeToJson(order), url);
    

    
    private string SerializeToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    
}