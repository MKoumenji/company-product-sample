using MediatR;

namespace Company.Product.Application.Contracts.Presentation.Commands;

public class LogCommand ():IRequest
{
    public string Message { get; set; } = string.Empty;
    public LogLevel Level { get; set; } = LogLevel.Information;
    public Exception? Exception { get; set; }

    public LogCommand(string message, LogLevel level = LogLevel.Information, Exception? exception = null) : this()
    {
        Message = message;
        Level = level;
        Exception = exception;
    }

}

public enum LogLevel
{
    Information
}
