using System.Data;
using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Infrastructure.Repository.Persistence.SqlDb;
using Dapper;

namespace Company.Product.Infrastructure.Services.OrderProcessing;

public class ProcessedOrders(ISqlDb sqlDb) : IRetrieveProcessedOrders
{
    // Fetching all OrderNumbers that match the provided orderIds
    public List<int> RetrieveById(List<int> orderIds)
    { 
        
        var orderIdsString = string.Join(",", orderIds);
        var parameters = new DynamicParameters();
        parameters.Add("@OrderIds", orderIdsString);
        var sql = "[Company_Product].[p_data_get_by_id]";
        
        
        
        return sqlDb.GetAll<int>(sql, parameters, CommandType.Text);
    }
    
}