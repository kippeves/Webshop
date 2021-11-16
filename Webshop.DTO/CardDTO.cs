using Newtonsoft.Json;

namespace Webshop.DTO
{
    public class CardDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("customer")]
        public int CustomerID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("cardno")]
        public long Number { get; set; }
        [JsonProperty("cvs")]
        public int CVS { get; set; }
    }
}
