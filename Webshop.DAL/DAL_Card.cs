using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Card
    {
        readonly IDataSource<CardDTO> _dataSource;
        public DAL_Card(IDataSource<CardDTO> dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<CardDTO> FindByCustomer(int id) {
            return _dataSource.LoadAll().Where(c => c.CustomerID == id);
        }
    }
}
