using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.DTO;

namespace Webshop.DataSource
{
   public class Source_Product : IDataSource<ProductDTO>
    {
        readonly string path = @"C:\Users\kippe\source\repos\Webshop\Webshop.Data\json\products.json";
        public bool Delete(ProductDTO _object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDTO> LoadAll()
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(json);
        }

        public ProductDTO LoadById(int i)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(json).Single(p=>p.Id == i);
        }

        public void Save(ProductDTO _object)
        {
            throw new NotImplementedException();
        }

        public ProductDTO Update(ProductDTO _object)
        {
            throw new NotImplementedException();
        }
    }
}
