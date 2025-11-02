using Newtonsoft.Json;

namespace Company.Product.Domain.ValueObjects
{
    /// <summary>
    /// This class implements the properties of shipping data
    /// </summary>
    public class ShippingData 
    {
       
        [JsonProperty("KepNumber", Order = 1)]
        public required string  KepNumber { get; set; }

        [JsonProperty("KepEmail", Order = 2)]
        public required string KepEmail { get; set; }

        [JsonProperty("Wishtime", Order = 3)]
        public required string Wishtime { get; set; }

     
    }
}
