using System.Threading.Tasks;

namespace Company.Product.Application.Contracts.Infrastructure;

public interface INotifierService
{
    Task SendErrorEmail(string subject, string errorMsg, string? stackTrace);
    
    Task SendWarningEmail(string subject, string warningMsg);
    
    
    Task SendInformationEmail(string? subject, string informationMsg);

}