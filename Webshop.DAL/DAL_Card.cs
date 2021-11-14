using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Card : IDataAccess<CardDTO>
    {
        readonly IDataSource<CardDTO> _dataSource;
        public DAL_Card(IDataSource<CardDTO> dataSource)
        {
            _dataSource = dataSource;
        }

        public CardDTO LoadById(int id)
        {
            return _dataSource.LoadById(id);
        }

        public void Delete(CardDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardDTO> LoadAll()
        {
            return _dataSource.LoadAll();
        }

        public void Save(CardDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(CardDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
