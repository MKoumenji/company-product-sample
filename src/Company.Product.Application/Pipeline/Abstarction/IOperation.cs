namespace Company.Product.Application.Pipeline.Abstarction;

public interface IOperation< in T>
{
    void Invoke(T data);
}