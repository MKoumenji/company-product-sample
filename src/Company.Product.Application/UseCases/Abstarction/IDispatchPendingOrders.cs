using Company.Product.Application.UseCases.Models;

namespace Company.Product.Application.UseCases.Abstarction;

public interface IDispatchPendingOrders
{
    public void Execute(InspectionResult inspectionResult,  string url, int pocessingTime);
}