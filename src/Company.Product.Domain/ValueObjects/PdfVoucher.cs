using Newtonsoft.Json;

namespace Company.Product.Domain.ValueObjects
{

    /// <summary>
    /// This class implements the properties of a voucher-pdf position
    /// </summary>
    public class PdfVoucher
    {
        /// <summary>
        /// Message-From 
        /// </summary>
        [JsonProperty("FromMsg", Order = 1)]
        public string? FromMsg { get; set; }

        /// <summary>
        /// Message-To
        /// </summary>
        [JsonProperty("ToMsg", Order = 2)]
        public string? ToMsg { get; set; }

        /// <summary>
        /// Message Text
        /// </summary>
        [JsonProperty("TextMsg", Order = 3)]
        public string? TextMsg { get; set; }
    }
}
