using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages
{
    public class IndexModel : PageModel
    {
        readonly   DAL_Product _productAccess;
        public     List<ProductDTO> products;
        public CustomerDTO SessionInfo_Customer { get; private set; }
        public const string SessionKeyCustomer = "_Customer";

        public IndexModel(DAL_Product productAccess)
        {
            _productAccess = productAccess;
        }

        public IActionResult OnGet()
        {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            if (SessionInfo_Customer != default) { 
                ViewData["username"] = SessionInfo_Customer.Name;
            }
            products = _productAccess.LoadAll().ToList();
            return Page()
        }

        public IActionResult OnPostSearch(string term) 
        {
            if (!string.IsNullOrEmpty(term))
            {
                products = _productAccess.LoadAll().Where(p => p.Name.ToLower().Contains(term.ToLower())).ToList();
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                return Page();
            }
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostSort() { 

        }

    }
}
