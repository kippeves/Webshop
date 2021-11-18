using System;
using System.Collections.Generic;
using System.Linq;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Product
    {
        readonly DataSource_JSON<ProductDTO> _dataSource;
        public DAL_Product(DataSource_JSON<ProductDTO> dataSource)
        {
            _dataSource = dataSource;
        }
        public void Delete(ProductDTO obj)
        {
            throw new NotImplementedException();
        }

        public ProductDTO LoadById( int i)
        {
            return _dataSource.LoadAll().SingleOrDefault(p=> p.Id == i);
        }

        public IEnumerable<ProductDTO> LoadAll()
        {
            return _dataSource.LoadAll();
        }

        public void Save(ProductDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(ProductDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
