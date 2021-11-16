using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User
{
    public class CartModel : PageModel
    {
        readonly DAL_Cart       _cartAccess;
        readonly DAL_Product    _productAccess;
        const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }
        public Dictionary<ProductDTO,int> Products { get; set; }
        public CartDTO UserCart { get; set; }

        public CartModel(DAL_Cart cartAccess, DAL_Product productAccess)
        {
            _cartAccess = cartAccess;
            _productAccess = productAccess;
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
                UserCart = _cartAccess.LoadById(currentCustomer.Id);
                if (UserCart != null){
                    foreach (var item in UserCart.Products)
                { 
                    Products.Add(_productAccess.LoadById(item.Key),item.Value);
                }
                }
            }
                return Page();
        }
        public ActionResult OnGetAdd(int id, int amt = 1)
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                CustomerDTO currentCustomer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                CartDTO UserCart = _cartAccess.LoadById(currentCustomer.Id);
                if (default == UserCart)
                {
                    UserCart = new CartDTO(currentCustomer.Id);
                    _cartAccess.Save(UserCart);
                }
                if (UserCart.Products.ContainsKey(id))
                {
                    UserCart.Products[id] += amt;
                }
                else
                {
                    UserCart.Products[id] = amt;
                }
                _cartAccess.Update(UserCart);
                return RedirectToPage("/User/Cart");
            }
        }
        public ActionResult OnGetRemove(int id, int amt = 1)
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                CustomerDTO currentCustomer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                CartDTO UserCart = _cartAccess.LoadById(currentCustomer.Id);
                if (default == UserCart)
                {
                    UserCart = new CartDTO(currentCustomer.Id);
                    _cartAccess.Save(UserCart);
                }
                if (UserCart.Products.ContainsKey(id))
                {
                    UserCart.Products[id] -= amt;
                    if (UserCart.Products[id] == 0)
                    {
                        UserCart.Products.Remove(id);
                    }
                }
                _cartAccess.Update(UserCart);
                return RedirectToPage("/User/Cart");
            }
        }
    }
}
