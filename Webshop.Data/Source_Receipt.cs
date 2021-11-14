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
  public  class Source_Receipt : IDataSource<ReceiptDTO>
    {
        readonly string path = @"C:\Users\kippe\source\repos\Webshop\Webshop.Data\json\receipts.json";
        public bool Delete(ReceiptDTO _object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReceiptDTO> LoadAll()
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<ReceiptDTO>>(json);
        }

        public ReceiptDTO LoadById(int i)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<ReceiptDTO>>(json).Single(r => r.id == i);
        }

        public void Save(ReceiptDTO _object)
        {
            throw new NotImplementedException();
        }

        public ReceiptDTO Update(ReceiptDTO _object)
        {
            throw new NotImplementedException();
        }
    }
}
