using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Interfaces;

namespace Company.Product.Infrastructure.Services.Notification;

public class Notifier(IEmailMessageBuilder? messageBuilder, IEmailSender? emailSender)
    : INotifierService
{
    public async Task SendErrorEmail(string subject, string errorMsg, string? stackTrace)
    {
        var emailMessage = messageBuilder?.BuildErrorMessage(subject, errorMsg, stackTrace);
        if (emailMessage != null)
            if (emailSender != null)
                await emailSender.SendAsync(emailMessage);
    }

    
    public async Task SendWarningEmail(string? subject, string warningMsg)
    {
        var emailMessage = messageBuilder?.BuildWarningMessage(subject, warningMsg);
        if (emailMessage != null)
            if (emailSender != null)
                await emailSender.SendAsync(emailMessage);

    }

    public async Task SendInformationEmail(string? subject, string informationMsg)
    {
        var emailMessage = messageBuilder?.BuildInformationMessage(subject, informationMsg);
        if (emailMessage != null)
            if (emailSender != null)
                await emailSender.SendAsync(emailMessage);
    }
}