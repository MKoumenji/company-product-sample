using System.Text;

namespace Company.Product.Infrastructure.Repository.ExternalServices.RestClient;

public class RestClient(HttpClient httpClient):IRestClient
{
    

    public async Task<HttpResponseMessage> PostAsync(string msg,string endpoint)
    {
     
        var content = new StringContent(msg, Encoding.Unicode, "application/json");
        return  await httpClient.PostAsync(endpoint, content);
    }
}