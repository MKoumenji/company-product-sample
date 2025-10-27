using Company.Product.Domain.ValueObjects;
using Newtonsoft.Json;

namespace Company.Product.Domain.Entities
{
    /// <summary>
    /// This class implements the properties of a position
    /// </summary>
    public class ShoppingCartItem
    {
        /// <summary>
        /// Position number of the item
        /// </summary>
        [JsonProperty("Pos", Order = 1)]
        public int Pos { get; set; }

        /// <summary>
        /// Variant name of the item
        /// </summary>
        [JsonProperty("variantName", Order = 2)]
        public required string VariantName { get; set; }

        /// <summary>
        /// Reference of the item
        /// </summary>
        [JsonProperty("unitReference", Order = 3)]
        public int UnitReference { get; set; }

        /// <summary>
        /// Original Unit price
        /// </summary>
        [JsonProperty("unitPrice", Order = 4)]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Payed price for a unit
        /// </summary>
        [JsonProperty("unitSalePrice", Order = 5)]
        public decimal UnitSalePrice { get; set; }

        /// <summary>
        /// Retail price for unit
        /// </summary>
        [JsonProperty("unitRetailPrice", Order = 6)]
        public decimal UnitRetailPrice { get; set; }

        /// <summary>
        /// Is the item Diccounted?
        /// </summary>
        /// <value> when value = 0 then not discounted else discountet</value>
        [JsonProperty("unitDiscount", Order = 7)]
        public int UnitDiscount { get; set; }

        /// <summary>
        /// Discount reason
        /// </summary>
        /// <value> 0 = no reason </value>
        [JsonProperty("unitDiscount_reason", Order = 8)]
        public int UnitDiscountReason { get; set; }

        /// <summary>
        /// Amount of saled items
        /// </summary>
        [JsonProperty("unitAmount", Order = 9)]
        public int UnitAmount { get; set; }

        /// <summary>
        /// Name of the brand
        /// </summary>
        [JsonProperty("articleBrandName", Order = 10)]
        public required string ArticleBrandName { get; set; }

        /// <summary>
        /// Additional properties of the unit
        /// </summary>
        /// <value>
        /// Null when no additional properties available
        /// </value>
        [JsonProperty("AdditionalProperties", Order = 11)]
        public AdditionalProperties? AdditionalProperties { get; set; }

    }
}
