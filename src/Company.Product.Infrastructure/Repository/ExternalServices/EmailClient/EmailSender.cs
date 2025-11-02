using Company.Product.Application.Contracts.Infrastructure;
using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Interfaces;
using Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Company.Product.Infrastructure.Repository.ExternalServices.EmailClient;

public class EmailSender(IOptions<MailKitConfig> mailKitConfigOptions, ISecLoggerService secLoggerService) : IEmailSender

{
    private readonly MailKitConfig _mailKitConfig = mailKitConfigOptions.Value;

    public async Task SendAsync(EmailMessage emailMessage)
    {
        var mimeMessage = new MimeMessage();

        // Von-Adressen
        mimeMessage.From.AddRange(emailMessage.FromAddresses.Select(address =>
            new MailboxAddress(address.Name, address.Address)));

        // An-Adressen
        mimeMessage.To.AddRange(emailMessage.ToAddresses.Select(address =>
            new MailboxAddress(address.Name, address.Address)));

        // Betreff und Inhalt
        mimeMessage.Subject = emailMessage.Subject;
        mimeMessage.Body =  new TextPart(TextFormat.Html) {Text = emailMessage.Content};

        // SMTP-Versand
        var smtpClient = new SmtpClient();
        try
        {
            
            await smtpClient.ConnectAsync(_mailKitConfig.SmtpServer, _mailKitConfig.SmtpPort,
                SecureSocketOptions.None, CancellationToken.None);
            smtpClient.AuthenticationMechanisms.Remove("GSSAPI"); //Remove any OAuth functionality as we won't be using it.
            await smtpClient.AuthenticateAsync(_mailKitConfig.SmtpUsername, _mailKitConfig.SmtpPassword);
            await smtpClient.SendAsync(mimeMessage);
            await Task.Delay(2000); 
        }catch (Exception ex)
        {
            secLoggerService.LogError("Company.Product-Error sending e-mail",ex);
        }
        finally
        {
            await smtpClient.DisconnectAsync(true);
        }

    }
}