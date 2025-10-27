using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Application.Contracts.Presentation.Commands;
using MediatR;

namespace Company.Product.Application.Services;

public class LogCommandHandler(ISecLoggerService logger) : IRequestHandler<LogCommand>
{
    public Task Handle(LogCommand request, CancellationToken cancellationToken)
    {
        switch (request.Level)
        {
            case LogLevel.Information:
                logger.LogInformation(request.Message);
                break;
        }
        return Task.CompletedTask;
    }
}