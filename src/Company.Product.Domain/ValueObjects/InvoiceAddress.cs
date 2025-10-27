using Newtonsoft.Json;

namespace Company.Product.Domain.ValueObjects
{
    /// <summary>
    /// This class implements the properties of the invoice adress
    /// </summary>
    public class InvoiceAddress 
    {

        [JsonProperty("street", Order = 1)]
        public required string Street { get; set; }

        [JsonProperty("houseNumber", Order = 2)]
        public required string HouseNumber { get; set; }

        [JsonProperty("company", Order = 3)]
        public string? Company { get; set; }

        [JsonProperty("additional", Order = 4)]
        public string? Additional { get; set; }

        [JsonProperty("zip", Order = 5)]
        public required string Zip { get; set; }

        [JsonProperty("city", Order = 6)]
        public required string City { get; set; }


        [JsonProperty("countryName", Order = 7)]
        public required string CountryName { get; set; }

        [JsonProperty("countryNumeric", Order = 8)]
        public int CountryNumeric { get; set; }

   



    }
}
