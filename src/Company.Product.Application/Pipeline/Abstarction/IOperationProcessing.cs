
namespace Company.Product.Application.Pipeline.Abstarction;

public interface IOperationProcessing
{
   internal Task<Task> ExecuteAsync(CancellationToken cancellationToken);

}