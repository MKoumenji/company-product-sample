using System.Data;
using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Domain.Contracts;
using Company.Product.Domain.Entities;
using Company.Product.Infrastructure.Repository.Persistence.SqlDb;
using Dapper;
using Newtonsoft.Json.Linq;

namespace Company.Product.Infrastructure.Services.OrderProcessing;

public class LoggedOrders(ISqlDb sqlDb, IOrderAddAttributService orderAddAttributService ) : IRetrieveLoggedOrders
{
    public List<RequestOrder> RetrieveByDate(DateTime from, DateTime to)
    {
        string sql = $"SELECT LogBody FROM FUTURERS.dbo.rocketapi_TransferObjectsRaw WHERE LogPath = '/api/v1/CustomerOrder/CustomerOrder'" + 
                     $"  AND LogDateTime >= '{from:yyyy-MM-dd HH:mm:ss}' AND LogDateTime <= '{to:yyyy-MM-dd HH:mm:ss}' ORDER BY LogDateTime DESC";
        
        List<string> fetchedOrders = sqlDb.GetAll<string>(sql, null, CommandType.Text).AsList();

        
        return fetchedOrders.Select(order => orderAddAttributService.AddShopAndSubShop(JObject.Parse(order).ToObject<RequestOrder>()!))
            .ToList();
    }
}