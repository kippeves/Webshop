using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User
{
    public class IndexModel : PageModel
    {
        readonly DAL_Customer   _customerAccess;
        readonly DAL_Order      _orderAccess;
        readonly DAL_Receipt    _receiptAccess;
        readonly DAL_Card       _cardAccess;

        public const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }

        public IndexModel(DAL_Customer customerAccess, DAL_Card cardAccess, DAL_Order orderAccess, DAL_Receipt receiptAccess)
        {
            _customerAccess = customerAccess;
            _cardAccess     = cardAccess;
            _orderAccess    = orderAccess;
            _receiptAccess  = receiptAccess;
        }

        public ActionResult OnGet()
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                return RedirectToPage("/Index");
            }
            else {
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                List<OrderDTO> customerOrders =_orderAccess.LoadByCustomer(SessionInfo_Customer.Id).ToList();
                using (_cardAccess.FindByCustomer(SessionInfo_Customer.Id);
                return Page();
            }
        }

        public ActionResult OnPostSet(int id) {
            HttpContext.Session.Set<CustomerDTO>(SessionKeyCustomer, _customerAccess.LoadById(id));
            return RedirectToPage("/User/Cart");
        }
    }
}
