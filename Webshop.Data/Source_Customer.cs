using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Webshop.DTO;

namespace Webshop.DataSource
{
   public class Source_Customer : IDataSource<CustomerDTO>
    {
        readonly string path = @"C:\Users\kippe\source\repos\Webshop\Webshop.Data\json\customers.json";
        public bool Delete(CustomerDTO _object)
        {
            // NO NEED
            return true;
        }

        public IEnumerable<CustomerDTO> LoadAll()
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<CustomerDTO>>(json);

        }

        public CustomerDTO LoadById(int i)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<CustomerDTO>>(json).Single(c=>c.Id == i);
        }

        public void Save(CustomerDTO _object)
        {
            // NO NEED
        }

        public CustomerDTO Update(CustomerDTO _object)
        {
            return null;
            // NO NEED
        }
    }
}
