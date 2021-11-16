using System;
using System.Collections.Generic;
using System.Linq;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Receipt
    {
        readonly IDataSource<ReceiptDTO>    _dataSource;
        public DAL_Receipt(IDataSource<ReceiptDTO> dataSource)
        {
            _dataSource   = dataSource;
        }
        public ReceiptDTO GetByKey(int cardId, int orderId)
        {
            return _dataSource.LoadAll().SingleOrDefault(r => (r.CardId == cardId) && (r.OrderId == orderId));
        }
        public IEnumerable<ReceiptDTO>SearchByCard(int card)
        {
            return _dataSource.LoadAll().Where(r => r.CardId == card);
        }
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
