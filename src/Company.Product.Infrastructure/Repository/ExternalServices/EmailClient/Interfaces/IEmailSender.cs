using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Models;

namespace Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Interfaces;

public interface IEmailSender
{
    Task SendAsync(EmailMessage emailMessage);

}