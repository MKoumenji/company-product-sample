
    namespace Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Models;

    /// <summary>
    /// This class implements  MailKitConfig to bind the Alert-Email service-parameters from app-setting Node (MailKitConfig)
    /// </summary>
    public class MailKitConfig
    {
        public required string? ServiceName { get; set; } 
        public required string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public required string SmtpUsername { get; set; }
        public required string SmtpPassword { get; set; }
        public required string?  EmailFrom{ get; set; }
        public required string EmailTo { get; set; }
    
    }