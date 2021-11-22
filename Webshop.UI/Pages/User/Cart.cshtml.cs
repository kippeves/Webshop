using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User
{
    public class CartModel : PageModel
    {
        readonly DAL_Cart _cartAccess;
        readonly DAL_Product _productAccess;
        readonly DAL_Card _cardAccess;
        private const string SessionKeyCustomer = "_Customer";
        private const string SessionKeyCart = "_Cart";

        public CustomerDTO SessionInfo_Customer { get; private set; }
        public CartDTO SessionInfo_Cart { get; private set; }
        public Dictionary<ProductDTO, int> Products { get; set; }
        public List<CardDTO> Cards { get; set; }

        public CartModel(DAL_Cart cartAccess, DAL_Product productAccess, DAL_Card cardAccess)
        {
            _cartAccess = cartAccess;
            _productAccess = productAccess;
            _cardAccess = cardAccess;
        }

        public void OnGet()
        {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            SessionInfo_Cart = HttpContext.Session.Get<CartDTO>(SessionKeyCart);
            Cards = new();
            Products = new();

            if (SessionInfo_Customer == default)
            {
                RedirectToPage("/Index");
            }
            else {
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                SessionInfo_Cart = HttpContext.Session.Get<CartDTO>(SessionKeyCart);
                foreach (var item in SessionInfo_Cart.Products)
                {
                    Products.Add(_productAccess.LoadById(item.Key), item.Value);
                }
            }
        }
    

    public IActionResult OnGetAdd(int id, int amt = 1)
    {
        if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
        {
            string host = HttpContext.Request.Headers["Referer"];
            return Redirect(host);
        }
        else
        {
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                SessionInfo_Cart = HttpContext.Session.Get<CartDTO>(SessionKeyCart);
                int amount;
            if (SessionInfo_Cart.Products.ContainsKey(id))
            {
                    SessionInfo_Cart.Products[id] += amt;
                    amount = SessionInfo_Cart.Products[id];
            }
            else
            {
                    SessionInfo_Cart.Products[id] = amt;
                    amount = SessionInfo_Cart.Products[id];
                }
            _cartAccess.Update(SessionInfo_Cart);
            HttpContext.Session.Set<CartDTO>(SessionKeyCart, SessionInfo_Cart);
                string result = "{ \"amount\" : "+amount+" }";
                return Content(result, "application/json");
        }
    }

        public IActionResult OnGetRemove(int id, int amt = 1)
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                string host = HttpContext.Request.Headers["Referer"];
                return Redirect(host);
            }
            else
            {
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                SessionInfo_Cart = HttpContext.Session.Get<CartDTO>(SessionKeyCart);
                int amount = 0;
                if (SessionInfo_Cart.Products.ContainsKey(id))
                {
                    SessionInfo_Cart.Products[id] -= amt;
                    amount = SessionInfo_Cart.Products[id];                    if (SessionInfo_Cart.Products[id] == 0)
                    {
                        SessionInfo_Cart.Products.Remove(id);
                    }
                    _cartAccess.Update(SessionInfo_Cart);
                    HttpContext.Session.Set<CartDTO>(SessionKeyCart, SessionInfo_Cart);
                }
                _cartAccess.Update(SessionInfo_Cart);
                HttpContext.Session.Set<CartDTO>(SessionKeyCart, SessionInfo_Cart);
                string result = "{ \"amount\" : " + amount + " }";
                return Content(result, "application/json");
            }
            }
        }
    }
