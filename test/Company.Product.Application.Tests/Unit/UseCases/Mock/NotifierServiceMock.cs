using Company.Product.Application.Contracts.Infrastructure;

namespace Company.Product.Application.Tests.Unit.UseCases.Mock;

public class NotifierServiceMock:INotifierService
{
    public Task SendErrorEmail(string subject, string errorMsg, string? stackTrace)
        => Task.CompletedTask;

    public Task SendWarningEmail(string subject, string warningMsg)
        => Task.CompletedTask;

    public Task SendInformationEmail(string? subject, string informationMsg)
        => Task.CompletedTask;
}