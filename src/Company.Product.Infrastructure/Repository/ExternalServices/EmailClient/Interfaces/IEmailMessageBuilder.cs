using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Models;

namespace Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Interfaces;

public interface IEmailMessageBuilder
{
    EmailMessage BuildErrorMessage(string? subject, string errorMsg, string? stackTrace);
    
    EmailMessage BuildWarningMessage(string? subject, string warningMsg);
    
    EmailMessage BuildInformationMessage(string? subject, string informationMsg);

}