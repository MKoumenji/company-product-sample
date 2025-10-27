using System.Data;
using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Domain.Entities;
using Company.Product.Infrastructure.Repository.Persistence.SqlDb;
using Newtonsoft.Json.Linq;

namespace Company.Product.Infrastructure.Services.OrderProcessing;

public class OrderHandlerHistory(ISqlDb sqlDb):IOrderHandlerHistory
{
    public List<RequestOrder> FetchUnprocessedOrders(List<int> orderIds)
    {
        var orderIdsString = string.Join(",", orderIds);
        var sql = $"SELECT order_request FROM  FUTURERS.orderhandler.history"
                  +$" WHERE order_id in ({orderIdsString}) AND order_inserted_to_futurers = 0";
        
        List<string> orders = sqlDb.GetAll<string>(sql, null, CommandType.Text);
        return orders.Select(order => JObject.Parse(order).ToObject<RequestOrder>()!).ToList();
    }

    public void DeleteUnprocessedOrdersByOrderId(List<int> orderIds)
    {
        var orderIdsString = string.Join(",", orderIds);
        var sql = $"Delete FROM  FUTURERS.orderhandler.history"
                  +$" WHERE order_id in ({orderIdsString}) AND order_inserted_to_futurers = 0";
        
         sqlDb.Delete<object>(sql, null!, CommandType.Text);
    }
}