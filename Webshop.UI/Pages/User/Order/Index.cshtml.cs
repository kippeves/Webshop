using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User.Order
{
    public class IndexModel : PageModel
    {

        public const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }
        public Dictionary<OrderDTO, Dictionary<ProductDTO, int>> ProductsPerOrder { get; set; }
        public List<OrderDTO> OrderPerCustomer { get; set; }
        private readonly DAL_Order _orderAccess;
        private readonly DAL_Product _productAccess;

        public IndexModel(DAL_Order orderAccess, DAL_Product productAccess)
        {
            _orderAccess = orderAccess;
            _productAccess = productAccess;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                ViewData["username"] = SessionInfo_Customer.Name;
                OrderPerCustomer = _orderAccess.LoadByCustomer(SessionInfo_Customer.Id).ToList();
                ProductsPerOrder = new();
                foreach (var Order in _orderAccess.LoadByCustomer(SessionInfo_Customer.Id))
                {
                    ProductsPerOrder[Order] = new();
                    foreach (var OrderRow in Order.Content)
                    {
                        ProductsPerOrder[Order][_productAccess.LoadById(OrderRow.Key)] = OrderRow.Value;
                    }
                }
            }
            return Page();
        }

    public IActionResult OnPostSort(string sortTerm) {
        if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
        {
            return RedirectToPage("/Index");
        }
        else
        {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            if (sortTerm == "asc") { 
                    OrderPerCustomer = _orderAccess.LoadByCustomer(SessionInfo_Customer.Id).OrderBy(o=>o.Is_paid).ToList();
            } else OrderPerCustomer = _orderAccess.LoadByCustomer(SessionInfo_Customer.Id).OrderByDescending(o => o.Is_paid).ToList();
            ProductsPerOrder = new();
            foreach (var Order in OrderPerCustomer)
            {
                ProductsPerOrder[Order] = new();
                foreach (var OrderRow in Order.Content)
                {
                    ProductsPerOrder[Order][_productAccess.LoadById(OrderRow.Key)] = OrderRow.Value;
                }
            }
        }
        return Page();
    }
}
}
