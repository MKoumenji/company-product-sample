using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Pipeline.Operations;

namespace Company.Product.Application.Pipeline.Models;

public class Pipeline<T> : IOperation<T>
{
    
    private readonly List<IOperation<T>> _operations = new ();
    
    // add operation at the end of the pipeline
    public void Register(IOperation<T> operation)
        => _operations.Add(operation);
    
    public void Invoke(T data)
    {
        foreach (var operation in _operations) 
            operation.Invoke(data);

    }
}