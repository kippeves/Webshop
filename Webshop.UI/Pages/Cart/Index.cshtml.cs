using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.Cart
{
    public class IndexModel : PageModel
    {

        readonly IDataAccess<CustomerDTO> _user;
        readonly IDataAccess<CartDTO> _carts;
        readonly IDataAccess<ProductDTO> _products;
        const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }

        public Dictionary<ProductDTO,int> Products { get; set; }

        public IndexModel(IDataAccess<CustomerDTO> user, IDataAccess<CartDTO> carts, IDataAccess<ProductDTO> products)
        {
            _user = user;
            _carts = carts;
            _products = products;
        }

        public ActionResult OnGet()
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                return RedirectToPage("/Index");
            }
            else {
                CustomerDTO currentCustomer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                Products = new();
                CartDTO userCart = _carts.LoadById(currentCustomer.Id);
                if (userCart != null){
                    foreach (var item in userCart.products)
                { 
                    Products.Add(_products.LoadById(item.Key),item.Value);
                }
                }
            }
                return Page();
        }

        public ActionResult OnGetAdd(int id)
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                return RedirectToPage("/Index");
            }
            else { 
            CustomerDTO currentCustomer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            CartDTO userCart = _carts.LoadById(currentCustomer.Id);
            if (default == userCart) {
                userCart = new CartDTO(currentCustomer.Id);
                _carts.Save(userCart);
            }
            if (userCart.products.ContainsKey(id))
            {
                userCart.products[id]++;
            }
            else { 
                userCart.products[id] = 1;
            }
                _carts.Update(userCart);
                return RedirectToPage("/Cart/Index");
            }
        }
    }
}
