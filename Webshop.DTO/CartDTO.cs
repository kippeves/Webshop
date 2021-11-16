using Newtonsoft.Json;
using System.Collections.Generic;

namespace Webshop.DTO
{
    public class CartDTO
    {
        public CartDTO(int customer)
        {
            Id = customer;
            Products = new Dictionary<int,int>();
        }

        //The user that owns the cart
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("products")]
        public IDictionary<int,int> Products { get; set; }
    }
}
