using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Webshop.DataSource
{
    public class DataSource_JSON<T>
    {
        readonly private string _PATH;

        public DataSource_JSON()
        {
            Dictionary<string, string> _PATHS = new()
            {
                { "Webshop.DTO.CardDTO",        @"C:\Users\kippe\Source\Repos\kippeves\Webshop\Webshop.Data\json\cards.json" },
                { "Webshop.DTO.CartDTO",        @"C:\Users\kippe\Source\Repos\kippeves\Webshop\Webshop.Data\json\carts.json"},
                { "Webshop.DTO.CustomerDTO",    @"C:\Users\kippe\Source\Repos\kippeves\Webshop\Webshop.Data\json\customers.json"},
                { "Webshop.DTO.OrderDTO",       @"C:\Users\kippe\Source\Repos\kippeves\Webshop\Webshop.Data\json\orders.json"},
                { "Webshop.DTO.ProductDTO",     @"C:\Users\kippe\Source\Repos\kippeves\Webshop\Webshop.Data\json\products.json"},
                { "Webshop.DTO.ReceiptDTO",     @"C:\Users\kippe\Source\Repos\kippeves\Webshop\Webshop.Data\json\receipts.json"}
            };
            _PATH = _PATHS[typeof(T).ToString()];
        }

        public IEnumerable<T> LoadAll()
        {
            string json = File.ReadAllText(_PATH);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }
        public void Update(IEnumerable<T> _ObjectsToUpdate)
        {
            List<T> ListOfItems = LoadAll().ToList();
            ListOfItems.Clear();
            ListOfItems.AddRange(_ObjectsToUpdate);
            File.WriteAllText(_PATH, JsonConvert.SerializeObject(ListOfItems));
        }
    }
}
