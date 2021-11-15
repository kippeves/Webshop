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
       [JsonProperty("key")]
       public string Key { get;set; }
    }
}
