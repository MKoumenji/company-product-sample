namespace Company.Product.Infrastructure.Repository.WebIntegration.WebAppManager;

public interface IWebAppManagerIis
{
 
    public bool  StartWebApp();
    public bool  StopWebApp();

}