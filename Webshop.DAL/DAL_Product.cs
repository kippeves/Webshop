using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Product
    {
        readonly IDataSource<ProductDTO> _dataSource;
        public DAL_Product(IDataSource<ProductDTO> dataSource)
        {
            _dataSource = dataSource;
        }
        public void Delete(ProductDTO obj)
        {
            throw new NotImplementedException();
        }

        public ProductDTO LoadById( int i)
        {
            return _dataSource.LoadById(i);
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
