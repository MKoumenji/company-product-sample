using Company.Product.Application.UseCases.Models;

namespace Company.Product.Application.UseCases.Abstarction;

public interface IValidateDispatchedOrders
{
    bool Execute(InspectionResult inspectionResult);
}