using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Webshop.DTO
{
    public class ReceiptDTO
    {
        [Required]
        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }
        [Required]
        [JsonProperty("card_id")]
        public int CardId { get; set; }
        [Required]
        [JsonProperty("order_id")]
        public int OrderId { get; set; }
        [JsonProperty("payment_date")]
        public DateTime Date_paid { get; set; } = DateTime.Now;
    }
}
