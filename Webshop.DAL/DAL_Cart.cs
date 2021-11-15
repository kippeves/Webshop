using System;
using System.Collections.Generic;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
   public class DAL_Cart : IDataAccess<CartDTO>
    {
        readonly IDataSource<CartDTO> _dataSource;
        public DAL_Cart(IDataSource<CartDTO> dataSource)
        {
            _dataSource = dataSource;
        }

        public CartDTO LoadById(int CustomerId) {
            return _dataSource.LoadById(CustomerId);
        }

        public void Delete(CartDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartDTO> LoadAll()
        {
            return _dataSource.LoadAll();
        }

 
        public void Save(CartDTO obj)
        {
            _dataSource.Save(obj);
        }

        public bool Update(CartDTO obj)
        {
            _dataSource.Update(obj);
            return true;
        }
    }
}
