using System.Data;
using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Domain.Entities;
using Company.Product.Infrastructure.Repository.Persistence.SqlDb;
using Dapper;
using Newtonsoft.Json.Linq;

namespace Company.Product.Infrastructure.Services.OrderProcessing;

public class OrderHandlerHistory(ISqlDb sqlDb):IOrderHandlerHistory
{
    public List<RequestOrder> FetchUnprocessedOrders(List<int> orderIds)
    {
     
        var orderIdsString = string.Join(",", orderIds);
        var parameters = new DynamicParameters();
        parameters.Add("@OrderIds", orderIdsString);
        var sql = "[Company_Product].[p_unprocessed_data_get_by_id]";
        
        List<string> orders = sqlDb.GetAll<string>(sql, parameters, CommandType.Text);
        return orders.Select(order => JObject.Parse(order).ToObject<RequestOrder>()!).ToList();
    }

    public void DeleteUnprocessedOrdersByOrderId(List<int> orderIds)
    {
  
        var orderIdsString = string.Join(",", orderIds);
        var parameters = new DynamicParameters();
        parameters.Add("@OrderIds", orderIdsString);
        var sql = "[Company_Product].[p_unprocessed_data_delete_by_id]";
         sqlDb.Delete<object>(sql, parameters, CommandType.Text);
    }
}