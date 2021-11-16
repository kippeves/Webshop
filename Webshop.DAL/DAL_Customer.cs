using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Customer
    {
        readonly IDataSource<CustomerDTO> _dataSource;
        public DAL_Customer(IDataSource<CustomerDTO> dataSource)
        {
            _dataSource = dataSource;
        }

        public CustomerDTO LoadById(int id)
        {
            return _dataSource.LoadById(id);
        }

        public void Delete(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerDTO> LoadAll()
        {
            return _dataSource.LoadAll();
        }

        public bool Update(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
