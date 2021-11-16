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
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("card_id")]
        public int CardId { get; set; }
        [JsonProperty("order_id")]
        public int OrderId { get; set; }
        [JsonProperty("payment_date")]
        public DateTime Date_paid { get; set; }
    }
}
