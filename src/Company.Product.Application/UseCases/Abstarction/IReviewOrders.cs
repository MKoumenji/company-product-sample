using Company.Product.Application.UseCases.Models;

namespace Company.Product.Application.UseCases.Abstarction;

public interface IReviewOrders
{
    
    public bool Execute(InspectionResult inspectionResult);

}