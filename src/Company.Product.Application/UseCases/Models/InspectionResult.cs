using Company.Product.Domain.Entities;

namespace Company.Product.Application.UseCases.Models;

public class InspectionResult
{
    public required List<RequestOrder> PendingOrders { get; set; } = new ();
    public required List<RequestOrder> LoggedOrders { get; set; }= new ();
    public required  List<int> ProcessedOrders { get; set; }= new ();

}