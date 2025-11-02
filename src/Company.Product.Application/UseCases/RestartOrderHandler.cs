using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Application.UseCases.Abstarction;

namespace Company.Product.Application.UseCases;

public class RestartOrderHandler(IOrderHandler orderHandler , ISecLoggerService secLoggerService, INotifierService notifierService) : IRestartOrderHandler
{
    public void Execute()
    {
        secLoggerService.LogWarning("Restart Order-Handler because of Freeze");
        notifierService.SendWarningEmail("company.product-Warning","Restarting Order-Handler because of freeze");
        orderHandler.Stop();
        orderHandler.Start();
    }
}