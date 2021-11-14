using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace Webshop.DTO
{
    public class CartDTO
    {
        //The user that owns the cart
        public int id { get; set; }        
        public IDictionary<int,int> products { get; set; }
    }
}
