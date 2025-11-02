using Newtonsoft.Json;

namespace Company.Product.Domain.ValueObjects
{
    /// <summary>
    /// This class implments the additional properties of a position in the order. 
    /// </summary>
    public class AdditionalProperties 
    {
        /// <value> The Value is Null if the positon is not a voucher-pdf </value>
        [JsonProperty("PDFVoucher", Order = 1)]
        public PdfVoucher? PdfVoucher { get; set; }


    }
}
