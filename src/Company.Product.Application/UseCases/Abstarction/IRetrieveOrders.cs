using Company.Product.Application.UseCases.Models;

namespace Company.Product.Application.UseCases.Abstarction;

public interface IRetrieveOrders
{
    public InspectionResult Execute(DateTime from, DateTime to);
}