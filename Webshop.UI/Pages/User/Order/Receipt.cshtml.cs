using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User.Order
{
    public class Receipt : PageModel
    {
        private readonly DAL_Receipt    _receiptAccess;
        private readonly DAL_Card       _cardAccess;
        private readonly DAL_Product    _productAccess;
        private readonly DAL_Order      _orderAccess;
        private const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }
        public List<ReceiptDTO> CustomerReceipts { get; set; }
        public Dictionary<OrderDTO,Dictionary<ProductDTO,int>> OrderProductQuantity{ get; set; }
        public Receipt(DAL_Receipt receiptAccess, DAL_Card cardAccess, DAL_Product productAccess, DAL_Order orderAccess)
        {
            _receiptAccess = receiptAccess;
            _cardAccess    = cardAccess;
            _productAccess = productAccess;
            _orderAccess   = orderAccess;
        }
        public void OnGet()
        {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            ViewData["username"] = SessionInfo_Customer.Name;
            OrderProductQuantity = new();
            CustomerReceipts = new();
            CustomerReceipts = _receiptAccess.GetByCustomer(SessionInfo_Customer.Id).ToList();
            foreach (ReceiptDTO Receipt in CustomerReceipts) {
                OrderDTO order = _orderAccess.LoadByKey(Receipt.OrderId,Receipt.CustomerId);
                OrderProductQuantity[order] = new();
                foreach (var row in order.Content) {
                    OrderProductQuantity[order].Add(_productAccess.LoadById(row.Key), row.Value);
                }
            }
        }
    }
}
