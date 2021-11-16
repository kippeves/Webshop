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
        readonly DAL_Customer _customerAccess;
        public const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }

        public IndexModel(DAL_Customer _customerAccess)
        {
            this._customerAccess = _customerAccess;
        }

        public ActionResult OnGet()
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                return RedirectToPage("/Index");
            }
            else {
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                return Page();
            }
        }

        public ActionResult OnPostSet(int id) {
            HttpContext.Session.Set<CustomerDTO>(SessionKeyCustomer, _customerAccess.LoadById(id));
            return RedirectToPage("/User/Cart");
        }
    }
}
