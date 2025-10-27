using System.Data;
using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Infrastructure.Repository.Persistence.SqlDb;

namespace Company.Product.Infrastructure.Services.OrderProcessing;

public class ProcessedOrders(ISqlDb sqlDb) : IRetrieveProcessedOrders
{
    // Fetching all OrderNumbers that match the provided orderIds
    public List<int> RetrieveById(List<int> orderIds)
    { 
        var sql = $" SELECT OrderNumber FROM  FUTURERS.dbo.rocketapi_CustomerOrderHead"
               +$" WHERE OrderNumber in ({string.Join(",", orderIds)})";
        
        return sqlDb.GetAll<int>(sql, null, CommandType.Text);
    }
    
}