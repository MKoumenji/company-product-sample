

using Newtonsoft.Json;

namespace Company.Product.Domain.Entities
{
    /// <summary>
    /// This class implements the properties of the customer
    /// </summary>
    public class Customer 
    {
        [JsonProperty("customerNumber", Order = 1)]
        public int CustomerNumber {get; set;}

        [JsonProperty("salutation", Order = 2)]
        public string? Salutation { get; set; }
       
        [JsonProperty("title", Order = 3)]
        public string? Title { get; set; }
       
        [JsonProperty("firstname",Order = 4)]
        public required string Firstname { get; set; }
        
        [JsonProperty("lastname", Order = 5)]
        public required string Lastname { get; set; }
        
        [JsonProperty("email", Order = 6)]
        public required string Email { get; set; }
        
        [JsonProperty("phone", Order = 7)]
        public string? Phone { get; set; }
       
        [JsonProperty("birthday", Order = 8)]
        public string? Birthday { get; set; }
       
        [JsonProperty("guest", Order = 9)]
        public int Guest { get; set; }

    }
}
