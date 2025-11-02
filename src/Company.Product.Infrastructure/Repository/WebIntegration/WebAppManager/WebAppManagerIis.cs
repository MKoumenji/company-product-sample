using Microsoft.Extensions.Options;
using Microsoft.Web.Administration;

namespace Company.Product.Infrastructure.Repository.WebIntegration.WebAppManager;

public class WebAppManagerIis(IOptions<IisConfig> iisConfig) : IWebAppManagerIis
{
    private  readonly int _waitTimeAfterStartInSec = iisConfig.Value.WaitingTimeStartInSec * 1000;
    private  readonly int _waitTimeAfterSTopInSec = iisConfig.Value.WaitingTimeStopInSec * 1000;
    private readonly string _siteName = iisConfig.Value.SiteName;
    private readonly string _poolName = iisConfig.Value.PoolName;
    public bool StartWebApp()
    {
        
        //Application Pool Start
        using var serverManager = new ServerManager();
        var applicationPool = serverManager.ApplicationPools[_poolName];


        if (applicationPool.State != ObjectState.Started)
        {
            applicationPool.Start();
            Thread.Sleep(_waitTimeAfterStartInSec);
            
            if(applicationPool.State != ObjectState.Started)
                throw new InvalidOperationException($"The Application Pool '{_poolName}' could not be Started.");
        }   
            
        
        
        // Website Start
        Site site = serverManager.Sites[_siteName];
        
        if (site.State == ObjectState.Started)
            return true;
            
        site.Start();
        Thread.Sleep(_waitTimeAfterStartInSec);
            
        if(site.State != ObjectState.Started)
            throw new InvalidOperationException($"Company.Product: The Web app '{_siteName}' could not be Started.");

        return true;
    }

    public bool StopWebApp()
    {
        
        // Site Stop
        using var serverManager = new ServerManager();
        Site site = serverManager.Sites[_siteName];

        if (site.State != ObjectState.Stopped)
        {
            site.Stop();
            Thread.Sleep(_waitTimeAfterSTopInSec);
            if(site.State != ObjectState.Stopped)
                throw new InvalidOperationException($"Company.Product-Error: The Web app '{_siteName}' could not be Stopped.");
        }
       
        //Application Pool Stop
        var applicationPool = serverManager.ApplicationPools[_poolName];
        
        if (applicationPool.State == ObjectState.Stopped)
            return true;
        
        
        applicationPool.Stop();
        Thread.Sleep(_waitTimeAfterSTopInSec);
            
        if(applicationPool.State != ObjectState.Stopped)
            throw new InvalidOperationException($"Company.Product-Error: The Application Pool '{_poolName}' could not be Stopped.");
        
        return true;

    }
}