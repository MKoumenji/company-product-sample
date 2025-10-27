using System;

namespace Company.Product.Application.Contracts.Infrastructure;

public interface ISecLoggerService
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? exception = null);
    
}