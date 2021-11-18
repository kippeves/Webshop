using Newtonsoft.Json;
using System;

namespace Webshop.DTO
{
    public class CustomerDTO
    {
       [JsonProperty("id")]
       public int Id { get; set; }
       [JsonProperty("name")]
       public string Name { get; set; }
       [JsonProperty("hash")]
       public string Hash { get;set; }
       [JsonProperty("salt")]
       public string Salt { get; set; }
    }
}
