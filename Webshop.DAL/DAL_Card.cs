using System.Collections.Generic;
using System.Linq;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Card
    {
        readonly DataSource_JSON<CardDTO> _dataSource;
        public DAL_Card(DataSource_JSON<CardDTO> dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<CardDTO> FindByCustomer(int id) {
            return _dataSource.LoadAll().Where(c => c.CustomerID == id);
        }
    }
}
