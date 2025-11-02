
	  namespace Company.Product.Infrastructure.Repository.ExternalServices.EmailClient.Models;

	 /// <summary>
	 /// This class defines the properties of the alert-email message.
	 /// </summary>
	 public class EmailMessage
	 {
		 public List<EmailAddress> FromAddresses { get; set; } = new List<EmailAddress>();
		 public List<EmailAddress> ToAddresses { get; set; } = new List<EmailAddress>();
		 public string? Subject { get; set; }
		 public string Content { get; set; } = string.Empty;

	 }