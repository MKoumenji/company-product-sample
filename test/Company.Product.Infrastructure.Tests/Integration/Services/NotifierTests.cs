using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient;
using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Interfaces;
using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Models;
using Company.Product.Infrastructure.Services.Logging;
using Company.Product.Infrastructure.Services.Notification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Company.Product.Infrastructure.Tests.Integration.Services;

public class NotifierTests
{
    private readonly IEmailSender? _emailSender;
    private readonly IEmailMessageBuilder? _messageBuilder;
    
    public NotifierTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings_infrastructure_tests.json", optional: false, reloadOnChange: true);
        var configuration = builder.Build();
        var mailKitConfig = configuration.GetSection(nameof(MailKitConfig)).Get<MailKitConfig>();
        if (mailKitConfig == null) return;
        var optionMailConfig = Options.Create(mailKitConfig);
        _emailSender = new EmailSender(optionMailConfig, new SecLoggerService());
        _messageBuilder = new EmailMessageBuilder(optionMailConfig);

    }
    
    [Fact]
    public async Task SendErrorEmail_WhenValidParameters_ShouldPass()
    {
        // Arrange
        INotifierService notifierService = new Notifier(_messageBuilder, _emailSender);
        string subject = "Test Subject";
        string errorMsg = "This is a test error message.";
        string stackTrace = "Stack trace details here.";

        // Act
        await notifierService.SendErrorEmail(subject, errorMsg, stackTrace);
        
        // Assert
     
    }
    
    
    [Fact]
    public async Task SendWarningEmail_WhenValidParameters_ShouldPass()
    {
        // Arrange
        INotifierService notifierService = new Notifier(_messageBuilder, _emailSender);
        string subject = "Test Subject";
        string warningMsg = "This is a test warning message.";

        // Act
        await notifierService.SendWarningEmail(subject, warningMsg);
        
        // Assert
 
    }
    
    
    
    [Fact]
    public async Task SendInformationEmail_WhenValidParameters_ShouldPass()
    {
        // Arrange
        INotifierService notifierService = new Notifier(_messageBuilder, _emailSender);
        string subject = "Test Subject";
        string infomationMsg = "This is a test info message.";

        // Act
        await notifierService.SendInformationEmail(subject, infomationMsg);
        
        // Assert
   
    }
}