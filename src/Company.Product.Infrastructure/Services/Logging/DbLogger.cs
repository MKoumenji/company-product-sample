using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Infrastructure.Repository.Persistence.SqlDb;
using Dapper;

namespace Company.Product.Infrastructure.Services.Logging;

public class DbLogger (ISqlDb sqlDb): IDbLoggerService
{
    
    public bool LogToDb(string order, int orderId, bool isProcessed)
    {
        var dbParams = new DynamicParameters();
        dbParams.Add("@OrderNumber", orderId);
        dbParams.Add("@OrderRequest", order);
        dbParams.Add("@IsProcessed ", isProcessed);
        
        var ret = sqlDb.Insert<int>("[Company_Product].[p_history_insert]", dbParams);
        return ret == 0;
    }
    
}