using System;
using System.Collections.Generic;
using System.Linq;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
   public class DAL_Cart
    {
        readonly DataSource_JSON<CartDTO> _dataSource;
        public DAL_Cart(DataSource_JSON<CartDTO>  dataSource)
        {
            _dataSource = dataSource;
        }

        public CartDTO LoadById(int Customer) {
            return _dataSource.LoadAll().SingleOrDefault(c => c.Id == Customer);
        }

        public void Delete(CartDTO obj)
        {
            var AllCustomers = _dataSource.LoadAll().ToList();
            AllCustomers.RemoveAll(c => c.Id == obj.Id);
            _dataSource.Update(AllCustomers);
        }

        public void Save(CartDTO obj)
        {
            var _cartSource = _dataSource.LoadAll().ToList();
            _cartSource.Add(obj);
            _dataSource.Update(_cartSource);
        }

        public void Update(CartDTO obj)
        {
            var AllCustomers = _dataSource.LoadAll().ToList();
            AllCustomers.RemoveAll(c => c.Id == obj.Id);
            AllCustomers.Add(obj);
            _dataSource.Update(AllCustomers);
        }
    }
}
