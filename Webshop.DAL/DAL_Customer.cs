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
        readonly DataSource_JSON<CustomerDTO> _dataSource;
        public DAL_Customer(DataSource_JSON<CustomerDTO> dataSource)
        {
            _dataSource = dataSource;
        }

        public CustomerDTO LoadById(int id)
        {
            return _dataSource.LoadAll().SingleOrDefault(c => c.Id == id);
        }
    }
}
