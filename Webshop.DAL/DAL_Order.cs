using System.Collections.Generic;
using System.Linq;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Order
    {
        readonly IDataSource<OrderDTO> _dataSource;
        public DAL_Order(IDataSource<OrderDTO> dataSource)
        {
            _dataSource = dataSource;
        }
        public OrderDTO LoadById(int i)
        {
            return _dataSource.LoadById(i);
        }
        /*
        public OrderDTO LoadByKey(int customer, int cart)
        {
            return _dataSource.LoadAll().Single(o => (o.CustomerId == customer) && (o.CartId == cart));
        }
        */
        public void Delete(OrderDTO obj)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<OrderDTO> LoadAll()
        {
            return _dataSource.LoadAll();
        }

/*        public IEnumerable<OrderDTO> LoadByCustomer(int CustomerId)
        {
            return _dataSource.LoadAll(). Where(o => o.CustomerId == CustomerId);
        }
*/
        public void Save(OrderDTO obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(OrderDTO obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
