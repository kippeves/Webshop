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
                { "Webshop.DTO.CardDTO",
                    System.AppDomain.CurrentDomain.BaseDirectory+@"\json\cards.json" },
                { "Webshop.DTO.CartDTO",
                    System.AppDomain.CurrentDomain.BaseDirectory+@"\json\carts.json" },
                { "Webshop.DTO.CustomerDTO",
                    System.AppDomain.CurrentDomain.BaseDirectory+ @"\json\customers.json" },
                { "Webshop.DTO.OrderDTO",
                    System.AppDomain.CurrentDomain.BaseDirectory+ @"\json\orders.json" },
                { "Webshop.DTO.ProductDTO",
                    System.AppDomain.CurrentDomain.BaseDirectory+ @"\json\products.json" },
                { "Webshop.DTO.ReceiptDTO",
                    System.AppDomain.CurrentDomain.BaseDirectory+ @"\json\receipts.json" }
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
