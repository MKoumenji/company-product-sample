namespace Company.Product.Infrastructure.Repository.WebIntegration.WebAppManager;

public class IisConfig
{

    public required string SiteName { get; set; }
    public required string PoolName { get; set; }
    public required int WaitingTimeStartInSec { get; set; }
    public required int WaitingTimeStopInSec { get; set; }
}