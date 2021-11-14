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
   public class Source_Order : IDataSource<OrderDTO>
    {
        readonly string path = @"C:\Users\kippe\source\repos\Webshop\Webshop.Data\json\orders.json";
        public bool Delete(OrderDTO _object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDTO> LoadAll()
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(json);
        }

        public OrderDTO LoadById(int i)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(json).Single(o=>o.Id == i);
        }

        public void Save(OrderDTO _object)
        {
            throw new NotImplementedException();
        }

        public OrderDTO Update(OrderDTO _object)
        {
            throw new NotImplementedException();
        }
    }
}
