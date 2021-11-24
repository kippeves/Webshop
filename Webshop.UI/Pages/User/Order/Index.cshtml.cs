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
        private readonly DAL_Order _orderAccess;
        private readonly DAL_Product _productAccess;

        public const string SessionKeyCustomer = "_Customer";

        //Nuvarande inloggande kund
        public CustomerDTO SessionInfo_Customer { get; private set; }

        // Används som kopplingsindex för att kunna se vilka produkter som finns i varje order.
        public Dictionary<OrderDTO, Dictionary<ProductDTO, int>> ProductsPerOrder { get; set; }
        // Samma som ovan, men för att se vilka ordrar en kund har.
        public List<OrderDTO> OrderPerCustomer { get; set; }

        public IndexModel(DAL_Order orderAccess, DAL_Product productAccess)
        {
            _orderAccess = orderAccess;
            _productAccess = productAccess;
        }


        /// <summary>
        /// Körs när en kund vill se alla ordrar. Om en kund inte är inloggad skickas den tillbaka till index-sidan.
        /// Mest som ett löst skydd om en person har försökt gå in på sidan när den inte har några sparade detaljer.
        /// </summary>
        /// <returns></returns>

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


        /// <summary>
        /// Körs när en kund vill sortera sina ordrar
        /// </summary>
        /// <param name="sortTerm">En sträng innehållande antingen "asc" eller "desc" beroende 
        /// på om man vill ha störst först eller minst först.</param>
        /// <returns></returns>
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
