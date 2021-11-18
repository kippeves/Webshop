using System.Collections.Generic;
using System.Linq;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Order
    {
        readonly DataSource_JSON<OrderDTO> _dataSource;
        public DAL_Order(DataSource_JSON<OrderDTO> dataSource)
        {
            _dataSource = dataSource;
        }
        public OrderDTO LoadById(int id)
        {
            return _dataSource.LoadAll().SingleOrDefault(order => order.Id == id);
        }
        public OrderDTO LoadByKey(int id, int customer)
        {
            return _dataSource.LoadAll().Single(o => (o.CustomerId == customer) && (o.Id == id));
        }

        public IEnumerable<OrderDTO> LoadAll()
        {
            return _dataSource.LoadAll();
        }

        public IEnumerable<OrderDTO> LoadByCustomer(int CustomerId)
        {
            return _dataSource.LoadAll(). Where(o => o.CustomerId == CustomerId);
        }
        

        public int PutOrder(CustomerDTO c, CartDTO cart)
        {
            var CustomerOrders = LoadByCustomer(c.Id);

            var OrderRepo = _dataSource.LoadAll().ToList();
            OrderDTO newOrder = new(CustomerOrders.Count(), c.Id, new Dictionary<int, int>(cart.Products));
            OrderRepo.Add(newOrder);
            _dataSource.Update(OrderRepo);
            return newOrder.Id;
        }

        public void Update(OrderDTO order)
        {
            var OrderRepo = _dataSource.LoadAll().ToList();
            OrderRepo.RemoveAll(temp=>temp.CustomerId == order.CustomerId && temp.Id == order.Id);
            OrderRepo.Add(order);
            _dataSource.Update(OrderRepo);
        }

        public void Pay(int order,int customer) {
            var OrderRepo = _dataSource.LoadAll().ToList();
            OrderDTO SelectOrder = OrderRepo.SingleOrDefault(o => o.CustomerId == customer && o.Id == order);
            if (SelectOrder != default) {
                SelectOrder.Is_paid = true;
            }
            Update(SelectOrder);
        }

    }
}
