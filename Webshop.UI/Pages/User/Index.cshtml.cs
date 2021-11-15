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
        [ViewData]
        public string Title { get; set; }
        readonly IDataAccess<CustomerDTO> _Customers;
        public const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set;}
        public CustomerDTO currentCustomer { get; set; }

        public IndexModel(IDataAccess<CustomerDTO> Customers)
        {
            _Customers = Customers;
            Title = "Index";
        }

        public ActionResult OnGet()
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                return RedirectToPage("/Index");
            }
            else {
                currentCustomer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                return Page();
            }
        }

        public ActionResult OnPostSet(int id) {
            HttpContext.Session.Set<CustomerDTO>(SessionKeyCustomer, _Customers.LoadById(id));
            return RedirectToPage("/User/Index");
        }
    }
}
