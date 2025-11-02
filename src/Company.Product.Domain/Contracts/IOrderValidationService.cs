using Company.Product.Domain.Entities;

namespace Company.Product.Domain.Contracts;

public interface IOrderValidationService
{
    

    /// <summary>
    /// Prüft, ob es neue Bestellungen gibt.
    /// </summary>
    /// <param name="loggedOrders">Liste der neuen Bestellungen aus dem Log.</param>
    /// <returns>True, wenn neue Bestellungen eingegangen sind.</returns>
    public bool HasNewOrders(List<RequestOrder> loggedOrders);
    
    
    
    /// <summary>
    // Prüft, ob alle Bestellungen erfolgreich verarbeitet wurden
    /// </summary>
    /// <param name="loggedOrders">Liste der Bestellungen aus dem Log.</param>
    /// <param name="processedOrders">Liste der Bestellungen aus dem ERP.</param>
    /// <returns>True, ob alle Bestellungen erfolgreich verarbeitet wurden</returns>
    public bool HasPendingOrders(List<RequestOrder> loggedOrders, List<int> processedOrders);

 
}