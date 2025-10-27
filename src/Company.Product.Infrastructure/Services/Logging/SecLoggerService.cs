using Company.Product.Application.Contracts.Infrastructure;
using Serilog;

namespace Company.Product.Infrastructure.Services.Logging;

public class SecLoggerService : ISecLoggerService
{
    public void LogInformation(string messageTemplate)
        => Log.Information(messageTemplate);
    
    public void LogWarning(string messageTemplate)
        => Log.Warning(messageTemplate);
    
    public void LogError(string messageTemplate, Exception? exception)
    {
        if (exception != null)
            Log.Error(exception, messageTemplate);
        else
            Log.Error(messageTemplate);
        
    }
    
    
}