using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User
{
    public class OrderModel : PageModel
    {
        readonly DAL_Order  _orderAccess;
        readonly DAL_Card   _cardAccess;
        readonly DAL_Cart   _cartAccess;
        public  List<CardDTO> Cards { get; set; }
        public const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }

        public OrderModel(DAL_Order orderAccess, DAL_Card cardAccess, DAL_Cart cartAccess)
        {
            _orderAccess    = orderAccess;
            _cardAccess     = cardAccess;
            _cartAccess     = cartAccess;
        }

        public ActionResult OnGet(int? id) {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            if (default == SessionInfo_Customer)
            {
                return RedirectToPage("/Index");
            }
            else {
                if (id != default)
                {
                    Cards = _cardAccess.FindByCustomer(SessionInfo_Customer.Id).ToList();
                    return Page();
                } else return RedirectToPage("/User/Index");
            }
        }

        public ActionResult OnGetPut(int cart) {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            if (default == SessionInfo_Customer)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                CartDTO c = _cartAccess.LoadById(cart);
                int orderNo = _orderAccess.PutOrder(SessionInfo_Customer,c);
                _cartAccess.Delete(c);
                return RedirectToPage("/User/Order", new { id = orderNo });
            }
        }
    }
}
