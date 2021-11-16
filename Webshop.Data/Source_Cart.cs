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
            return LoadAll().SingleOrDefault(cart=>cart.Id == i);
        }

        public void Save(CartDTO _object)
        {
            List<CartDTO> carts = LoadAll().ToList();
            carts.Add(_object);
            File.WriteAllText(path,JsonConvert.SerializeObject(carts));
        }

        public CartDTO Update(CartDTO _object)
        {
            List<CartDTO> carts = LoadAll().ToList();
            carts.RemoveAll(c => c.Id == _object.Id);
            carts.Add(_object);
            File.WriteAllText(path, JsonConvert.SerializeObject(carts));
            return _object;
        }
    }
}
