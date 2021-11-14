using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Webshop.DTO;

namespace Webshop.DataSource
{
    public class Source_Cart : IDataSource<CartDTO>
    {
        readonly string path = @"C:\Users\kippe\source\repos\Webshop\Webshop.Data\json\carts.json";

        public bool Delete(CartDTO _object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartDTO> LoadAll()
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<CartDTO>>(json);
        }

        public CartDTO LoadById(int i)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<CartDTO>>(json).Single(c=>c.id == i);
        }

        public void Save(CartDTO _object)
        {
            throw new NotImplementedException();
        }

        public CartDTO Update(CartDTO _object)
        {
            throw new NotImplementedException();
        }
    }
}
