using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User
{
    public class OrderModel : PageModel
    {
        readonly DAL_Order _orderAccess;
        public const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }

        public OrderModel(DAL_Order orderAccess)
        {
            _orderAccess = orderAccess;
        }

        public ActionResult OnGet() {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            if (default == SessionInfo_Customer)
            {
                return RedirectToPage("/Index");
            }
            else {
                return Page();
            }
        }
    }
}
