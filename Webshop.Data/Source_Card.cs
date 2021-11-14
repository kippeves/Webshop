using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Webshop.DTO;

namespace Webshop.DataSource
{
    public class Source_Card : IDataSource<CardDTO>
    {
        readonly string path = @"C:\Users\kippe\source\repos\Webshop\Webshop.Data\json\cards.json";
        public bool Delete(CardDTO _object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardDTO> LoadAll()
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<CardDTO>>(json);
        }

        public CardDTO LoadById(int i)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<CardDTO>>(json).Single(c=>c.CustomerRefID==i);
        }

        public void Save(CardDTO _object)
        {
            throw new NotImplementedException();
        }

        public CardDTO Update(CardDTO _object)
        {
            throw new NotImplementedException();
        }
    }
}
