using Company.Product.Domain.ValueObjects;
using Newtonsoft.Json;

namespace Company.Product.Domain.Entities
{

    /// <summary>
    /// This class implements the structure of Order-Request, <br/>
    /// it is used to bind a json-request coming from the web shop 
    /// </summary>
    public class RequestOrder
    {
        /// <summary>
        /// This property is used to bind the id of the Order
        /// </summary>
        /// <return> Returns int </return>
        [JsonProperty("OrderNumber", Order = 1 )]
        public int OrderNumber { get; set; }


        /// <summary>
        /// This property is used to bind the creation date of the Order
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("OrderDate", Order = 2)]
        public required string OrderDate { get; set; }

        /// <summary>
        /// This property is used to bind the currency used by the customer
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("Currency", Order = 3)]
        public required string Currency { get; set; }

        /// <summary>
        /// This property is used to bind the customer-properties
        /// </summary>
        /// <return> Returns RequestCustomerOrderCustomer </return>
        [JsonProperty("Customer", Order = 4)]
        public required Customer Customer { get; set; }

        /// <summary>
        /// This property is used to bind the address of the invoice
        /// </summary>
        /// <return> Returns RequestCustomerOrderInvoiceAddress </return>
        [JsonProperty("InvoiceAddress", Order = 5)]
        public required InvoiceAddress InvoiceAddress { get; set; }

        /// <summary>
        /// This property is used to bind the address of the delivery
        /// </summary>
        /// <return> Returns RequestCustomerOrderDeliveryAddress </return>
        [JsonProperty("DeliveryAddress", Order = 6)]
        public required DeliveryAddress DeliveryAddress { get; set; }

        /// <summary>
        /// This property is used to bind shopping cart
        /// </summary>
        /// <return> Returns List of RequestCustomerOrderShoppingCartItem </return>
        [JsonProperty("ShoppingCart", Order = 7)]
        public required List<ShoppingCartItem> ShoppingCart { get; set; }


        /// <summary>
        /// This property is used to bind the type of the shipping
        /// </summary>
        /// <return> Returns RequestCustomerOrderShippingType </return>
        [JsonProperty("ShippingType", Order = 8)]
        public required ShippingType ShippingType { get; set; }

        /// <summary>
        /// This property is used to bind the data of the shipping
        /// </summary>
        /// <return> Returns RequestCustomerOrderShippingData </return>
        [JsonProperty("ShippingData", Order = 9)]
        public required ShippingData ShippingData { get; set; }

        /// <summary>
        /// This property is used to bind the tax value
        /// </summary>
        /// <value> values:  1 or 0 </value>
        /// <return> Returns int </return>
        [JsonProperty("Tax", Order = 10)]
        public int Tax { get; set; }

        /// <summary>
        /// This property is used to bind the id of the partner
        /// </summary>
        /// <return> Returns int </return>
        [JsonProperty("PartnerId", Order = 11)]
        public int PartnerId { get; set; }

        /// <summary>
        /// This property is used to bind the id of the type of payment 
        /// </summary>
        /// <return> Returns int </return>
        [JsonProperty("PaymentTypeId", Order = 12)]
        public int PaymentTypeId { get; set; }

        /// <summary>
        /// This property is used to bind the detail of the payment
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("PaymentTypeDetail", Order = 13)]
        public required string PaymentTypeDetail { get; set; }


        /// <summary>
        /// This property is used to bind the Payment Provider Sum
        /// </summary>
        /// <return> Returns decimal </return>
        [JsonProperty("PaymentProviderSum", Order = 14)]
        public decimal PaymentProviderSum { get; set; }

        /// <summary>
        /// This property is used to bind the transaction id of the Payment
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("TransactionId", Order = 15)]
        public string? TransactionId { get; set; }

        /// <summary>
        /// This property is used to bind the token of the credit card
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("CreditCardToken", Order = 16)]
        public string? CreditCardToken { get; set; }


        /// <summary>
        /// This property is used to bind the credit card number <br/>
        /// used for paying the order 
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("CreditCardNumber", Order = 17)]
        public string? CreditCardNumber { get; set; }

        /// <summary>
        /// This property is used to bind the credit card holder
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("CreditCardHolder", Order = 18)]
        public string? CreditCardHolder { get; set; }

        /// <summary>
        /// This property is used to bind the expiration date of the credit card
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("CreditCardExpire", Order = 19)]
        public string? CreditCardExpire { get; set; }


        /// <summary>
        /// This property is used to bind the boni score of the customer
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("BoniScore", Order = 20)]
        public int BoniScore { get; set; }

        /// <summary>
        /// This property is used to bind the date of the boni score
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("ScoreDate", Order = 21)]
        public required string ScoreDate { get; set; }

        /// <summary>
        /// This property is used to bind the text of the boni score
        /// </summary>
        /// <return> Returns string </return>
        [JsonProperty("ScoreText", Order = 21)]
        public required string ScoreText { get; set; }

        /// <summary>
        /// This property is used to bind the id of the store
        /// </summary>
        /// <return> Returns int </return>
        [JsonProperty("Filiale", Order = 22)]
        public int Filiale { get; set; }

        /// <summary>
        /// This property is used to bind the id of the subshop
        /// </summary>
        /// <return> Returns int </return>
        [JsonProperty("Subshop", Order = 23)]
        public int Subshop { get; set; }
        

        [JsonProperty("DubletteDifferentEmail", Order = 24)]
        public int DubletteDifferentEmail { get; set; }

        

        /// <summary>
        /// This property is used to check if the request is saved to the data base
        /// </summary>
        /// <return> Returns long </return>
        [JsonIgnore]
        public long OrderHistoryId { get; set; }


        /// <summary>
        /// This property is used to update the processing status of the request
        /// </summary>
        /// <return> Returns bool </return>
        [JsonIgnore]
        public bool IsOrderInsertdToDb { get; set; }
    }
}
