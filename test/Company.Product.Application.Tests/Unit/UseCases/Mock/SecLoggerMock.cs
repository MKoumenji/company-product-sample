using Company.Product.Application.Contracts.Infrastructure;

namespace Company.Product.Application.Tests.Unit.UseCases.Mock;

public class SecLoggerMock: ISecLoggerService

{
    public List<string> InformationLogs { get; } = new();
    public List<string> WarningLogs { get; } = new();
    public List<(string Message, Exception? Exception)> ErrorLogs { get; } = new();

    public void LogInformation(string messageTemplate)
    {
        InformationLogs.Add(messageTemplate);
    }

    public void LogWarning(string messageTemplate)
    {
        WarningLogs.Add(messageTemplate);
    }

    public void LogError(string messageTemplate, Exception? exception)
    {
        ErrorLogs.Add((messageTemplate, exception));
    }

    public void Clear()
    {
        InformationLogs.Clear();
        WarningLogs.Clear();
        ErrorLogs.Clear();
    }

}