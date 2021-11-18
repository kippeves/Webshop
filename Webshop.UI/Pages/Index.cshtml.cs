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
        readonly   DAL_Product _dataAccess;
        public     List<ProductDTO> products;
        public CustomerDTO SessionInfo_Customer { get; private set; }
        public const string SessionKeyCustomer = "_Customer";

        public IndexModel(DAL_Product dataAccess)
        {
            _dataAccess = dataAccess;
            products= _dataAccess.LoadAll().ToList();
        }

        public void OnGet()
        {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
        }
    }
}
