using System;
using System.Collections.Generic;
using System.Linq;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Receipt : IDataAccess<ReceiptDTO>
    {
        readonly IDataSource<ReceiptDTO>    _dataSource;
        public DAL_Receipt(IDataSource<ReceiptDTO> dataSource)
        {
            _dataSource   = dataSource;
        }

        public ReceiptDTO LoadById(int i)
        {
            return _dataSource.LoadById(i);
        }
/*
        public ReceiptDTO GetByKey(int cardId, int orderId)
        {
            return _dataSource.LoadAll().Single(r => (r.CardId == cardId) && (r.OrderId == orderId));
        }
*/
        public void Delete(ReceiptDTO obj)
        {
            throw new NotImplementedException();
        }

/*        public IEnumerable<ReceiptDTO>LoadByCard(int card)
        {
            return _dataSource.LoadAll().Where(r => r.CardId == card);
        }*/
        public IEnumerable<ReceiptDTO> LoadAll()
        {
            return _dataSource.LoadAll();        
        }

        public void Save(ReceiptDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(ReceiptDTO obj)
        {
            throw new NotImplementedException();
        }

    }
}
