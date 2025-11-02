namespace Company.Product.Application.Contracts.Infrastructure;


public interface IDbLoggerService
{
  public  bool LogToDb(string order, int orderId, bool isProcessed);
}