using Newtonsoft.Json;

namespace Company.Product.Domain.ValueObjects
{
    /// <summary>
    /// This class implements the properties of the delivery adress
    /// </summary>
    public class DeliveryAddress 
    {
        [JsonProperty("firstname", Order = 1 )]
        public string? Firstname { get; set; }

        [JsonProperty("lastname", Order = 2)]
        public string? Lastname { get; set; }

        [JsonProperty("packstation", Order = 3)]
        public string? Packstation {get; set;}

        [JsonProperty("packstationNumber", Order = 4)]
        public string? PackstationNumber { get; set; }

        [JsonProperty("postNumber", Order = 5)]
        public string? PostNumber { get; set; }

        [JsonProperty("street", Order = 6)]
        public string? Street { get; set; }

        [JsonProperty("houseNumber", Order = 7)]
        public string? HouseNumber { get; set; }

        [JsonProperty("company", Order = 8)]
        public string? Company { get; set; }

        [JsonProperty("additional", Order = 9)]
        public string? Additional { get; set; }

        [JsonProperty("zip", Order = 10)]
        public string? Zip { get; set; }

        [JsonProperty("city", Order = 11)]
        public string? City { get; set; }

        [JsonProperty("countryNumeric", Order = 12)]
        public int CountryNumeric { get; set; }

        [JsonProperty("countryName", Order = 13)]
        public string? CountryName { get; set; }

        [JsonProperty("store", Order = 14)]
        public int Store { get; set; }

    }
}
