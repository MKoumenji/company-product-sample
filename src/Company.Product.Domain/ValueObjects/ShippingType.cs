using Newtonsoft.Json;

namespace Company.Product.Domain.ValueObjects
{
    /// <summary>
    /// This class implements the properties of the orders shipping type
    /// </summary>
    public class ShippingType 
    {
        /// <summary>
        /// Shipping type
        /// </summary>
        [JsonProperty("Id", Order = 1)]
        public required string Id { get; set; }

        /// <summary>
        /// Description of Shipping type
        /// </summary>
        [JsonProperty("Description", Order = 2)]
        public required string Description { get; set; }

        /// <summary>
        /// Shipping price
        /// </summary>
        [JsonProperty("Price", Order = 3)]
        public decimal Price { get; set; }

        /// <summary>
        /// Tax on Shipping price
        /// </summary>
        [JsonProperty("Tax", Order = 4)]
        public required string Tax { get; set; }

 
    }
}
