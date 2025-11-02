using Company.Product.Application.Contracts.Presentation.Commands;
using MediatR;

namespace Company.Product.Presentation;
public class Worker(IMediator mediator ) : BackgroundService
{
    
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        mediator.Send(new LogCommand(
            $"****************************************\nCheck started at: " 
            + $"{DateTimeOffset.Now:dd.MM.yyyy HH:mm:ss}"), cancellationToken
            );

        while (!cancellationToken.IsCancellationRequested)
            mediator.Send(new ProcessWorkflowCommand(), cancellationToken);
        
        mediator.Send(new LogCommand(
            $"Worker stopped at: {DateTimeOffset.Now}"), 
            cancellationToken);
        
        return Task.CompletedTask;
    }
}
