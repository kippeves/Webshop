using Newtonsoft.Json;

namespace Webshop.DTO
{
    public class ProductDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("image_url")]
        public string Image { get; set; }
        public int Weight { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
    }
}
