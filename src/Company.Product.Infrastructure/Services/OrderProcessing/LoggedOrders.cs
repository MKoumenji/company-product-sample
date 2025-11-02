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
        string sql = "[Company_Product].[p_data_get_by_date]";
        
        List<string> fetchedOrders = sqlDb.GetAll<string>(sql, null, CommandType.Text).AsList();

        
        return fetchedOrders.Select(order => orderAddAttributService.AddShopAndSubShop(JObject.Parse(order).ToObject<RequestOrder>()!))
            .ToList();
    }
}