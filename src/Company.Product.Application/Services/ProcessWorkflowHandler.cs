using Company.Product.Application.Contracts.Presentation.Commands;
using Company.Product.Application.Pipeline.Abstarction;
using MediatR;


namespace Company.Product.Application.Services;
public class ProcessWorkflowHandler(IOperationProcessing operationProcessing): IRequestHandler<ProcessWorkflowCommand>
{ 
        public async Task Handle(ProcessWorkflowCommand request, CancellationToken cancellationToken)
        =>  await operationProcessing.ExecuteAsync(cancellationToken);
}