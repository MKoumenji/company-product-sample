namespace Company.Product.Infrastructure.Repository.ExternalServices.RestClient;

public interface IRestClient
{
    public  Task<HttpResponseMessage>  PostAsync(string msg, string endpoint);
}