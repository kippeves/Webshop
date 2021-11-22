using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DTO
{
    public class ReceiptDTO
    {
        public ReceiptDTO(int customer,int order, int card)
        {
            CustomerId = customer; ;
            OrderId = order;
            CardId = card;
            Date_paid = DateTime.Now;
        }

        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty("card_id")]
        public int CardId { get; set; }
        [JsonProperty("order_id")]
        public int OrderId { get; set; }
        [JsonProperty("payment_date")]
        public DateTime Date_paid { get; set; }
    }
}
