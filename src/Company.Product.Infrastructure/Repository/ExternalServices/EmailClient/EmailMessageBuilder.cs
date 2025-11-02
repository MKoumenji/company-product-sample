using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Interfaces;
using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Models;
using Microsoft.Extensions.Options;

namespace Company.Product.Infrastructure.Repository.ExternalServices.EmailClient;

public class EmailMessageBuilder(IOptions<MailKitConfig> mailKitConfigOptions) : IEmailMessageBuilder
{
    
    private readonly MailKitConfig _mailKitConfig = mailKitConfigOptions.Value;
    
    
    public EmailMessage BuildErrorMessage( string? subject, string errorMsg, string? stackTrace)
    {
        if (string.IsNullOrEmpty(subject)) 
            subject = "Company.Product-Mailing-Error: No Subject Provided";

        var emailMessage = new EmailMessage();

        emailMessage.FromAddresses.Add(new EmailAddress
        {
            Name = _mailKitConfig.ServiceName!,
            Address = _mailKitConfig.EmailFrom!
        });

        emailMessage.ToAddresses.Add(new EmailAddress
        {
            Name = _mailKitConfig.ServiceName!,
            Address = _mailKitConfig.EmailTo
        });

        emailMessage.Subject = $"[ERROR] {subject}";
        emailMessage.Content = MsgErrorFormatter(subject, errorMsg, stackTrace);

        return emailMessage;

    }

    public EmailMessage BuildWarningMessage(string? subject, string warningMsg)
    {
        if (string.IsNullOrEmpty(subject)) 
            subject = "No Subject Provided";

        var emailMessage = new EmailMessage();

        emailMessage.FromAddresses.Add(new EmailAddress
        {
            Name = _mailKitConfig.ServiceName!,
            Address = _mailKitConfig.EmailFrom!
        });

        emailMessage.ToAddresses.Add(new EmailAddress
        {
            Name = _mailKitConfig.ServiceName!,
            Address = _mailKitConfig.EmailTo
        });

        emailMessage.Subject = $"[Warining] {subject}";
        emailMessage.Content = MsgWarningFormatter(subject, warningMsg);

        return emailMessage;
    }



    public EmailMessage BuildInformationMessage(string? subject, string informationMsg)
    {
        if (string.IsNullOrEmpty(subject)) 
            subject = "No Subject Provided";

        var emailMessage = new EmailMessage();

        emailMessage.FromAddresses.Add(new EmailAddress
        {
            Name = _mailKitConfig.ServiceName!,
            Address = _mailKitConfig.EmailFrom!
        });

        emailMessage.ToAddresses.Add(new EmailAddress
        {
            Name = _mailKitConfig.ServiceName!,
            Address = _mailKitConfig.EmailTo
        });

        emailMessage.Subject = $"[Information] {subject}";
        emailMessage.Content = MsgInformationFormatter(subject, informationMsg);

        return emailMessage;
    }

    private static string MsgInformationFormatter(string? subject, string informationMsg)
    {
        return $@"<!DOCTYPE html>
        <html>
        <head>
            <title>Information Notification</title>
            <style>
                body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                .info-header {{ color: #0099ff; font-size: 18px; font-weight: bold; margin-bottom: 20px; }}
                .section-title {{ font-weight: bold; margin-top: 20px; }}
            </style>
        </head>
        <body>
            <div class='info-header'>Information: {subject}</div>
            <p><span class='section-title'>Details:</span> {informationMsg}</p>
        </body>
        </html>";
    }

    private static string MsgWarningFormatter(string? subject, string warningMsg)
    {
        return $@"<!DOCTYPE html>
        <html>
        <head>
            <title>Warning Notification</title>
            <style>
                body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                .warning-header {{ color: #FFA500; font-size: 18px; font-weight: bold; margin-bottom: 20px; }}
                .section-title {{ font-weight: bold; margin-top: 20px; }}
            </style>
        </head>
        <body>
            <div class='warning-header'>A warning occurred: {subject}</div>
            <p><span class='section-title'>Warning Message:</span> {warningMsg}</p>
        </body>
        </html>";
    }

  

    private static string MsgErrorFormatter(string? subject, string errorMsg, string? stackTrace)
    {
        return $@"<!DOCTYPE html>
        <html>
        <head>
            <title>Error Notification</title>
            <style>
                body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                .error-header {{ color: #ff0000; font-size: 18px; font-weight: bold; margin-bottom: 20px; }}
                .section-title {{ font-weight: bold; margin-top: 20px; }}
                pre {{ 
                    background-color: #f9f9f9; 
                    padding: 15px; 
                    border: 1px solid #ddd; 
                    border-radius: 5px; 
                    font-family: Consolas, 'Courier New', monospace; 
                    font-size: 14px; 
                    color: #333; 
                    overflow-x: auto; 
                    white-space: pre-wrap; /* Zeilenumbrüche innerhalb des Inhalts */ 
                    word-wrap: break-word; /* Für besonders lange Stacktraces */ 
                }}
                code {{ color: #d63384; font-weight: bold; }}
            </style>
        </head>
        <body>
            <div class='error-header'>An error occurred: {subject}</div>
            <p><span class='section-title'>Error Message:</span> {errorMsg}</p>
            <p><span class='section-title'>Stack Trace:</span></p>
            <pre>{stackTrace}</pre>
        </body>
        </html>";
    }


}