﻿using System;
using System.Collections.Generic;
using System.Linq;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.DAL
{
    public class DAL_Receipt
    {
        readonly DataSource_JSON<ReceiptDTO>    _dataSource;
        public DAL_Receipt(DataSource_JSON<ReceiptDTO> dataSource)
        {
            _dataSource   = dataSource;
        }
        public IEnumerable<ReceiptDTO> GetByCustomer(int CustomerId)
        {
            return _dataSource.LoadAll().Where(r => r.CustomerId == CustomerId);
        }

        public IEnumerable<ReceiptDTO> LoadAll()
        {
            return _dataSource.LoadAll();        
        }

        public void MakeReceipt(int CustomerId, int OrderId, int CardId) {
            ReceiptDTO receipt = new(CustomerId, OrderId, CardId);
            Save(receipt);
        }

        public void Save(ReceiptDTO obj)
        {
            var _recipes = _dataSource.LoadAll().ToList();
            _recipes.Add(obj);
            _dataSource.Update(_recipes);
        }

        public bool Update(ReceiptDTO obj)
        {
            throw new NotImplementedException();
        }

    }
}
