using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User
{
    public class CartModel : PageModel
    {
        readonly DAL_Cart _cartAccess;
        readonly DAL_Product _productAccess;
        private const string SessionKeyCustomer = "_Customer";
        private const string SessionKeyCart = "_Cart";

        public CustomerDTO SessionInfo_Customer { get; private set; }
        public CartDTO SessionInfo_Cart { get; private set; }
        public Dictionary<ProductDTO, int> Products { get; set; }
        public List<CardDTO> Cards { get; set; }

        public CartModel(DAL_Cart cartAccess, DAL_Product productAccess)
        {
            _cartAccess = cartAccess;
            _productAccess = productAccess;
        }
        // Visa kundvagn
        public void OnGet()
        {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            SessionInfo_Cart = HttpContext.Session.Get<CartDTO>(SessionKeyCart);
            Products = new();
            // Finns ingen kund inloggad, så skicka tillbaka till index.
            if (SessionInfo_Customer == default)
            {
                RedirectToPage("/Index");
            }
            else { // Hämta lagrad kund och kundvagn
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                SessionInfo_Cart = HttpContext.Session.Get<CartDTO>(SessionKeyCart);
                foreach (var item in SessionInfo_Cart.Products)
                { // Skapa en lista med produkter, baserat på kundvagnen.
                    Products.Add(_productAccess.LoadById(item.Key), item.Value);
                }
            }
        }
    

        /// <summary>
        /// Lägger till en produkt i kundvagnen. Körs när en kund trycker på 1. Lägg till produkt, eller 2. När den trycker på uppåtpilen i kundvagnen.
        /// Tanken var att lägga till en valfri mängd produkter att kunna öka med, men jag lade aldrig till det i gränssnittet.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amt"></param>
        /// <returns>Ett JSonResult-objekt innehållande {"amount": mängden som produkten hade när den var uppdaterad}</returns>
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


        /// <summary>
        /// Tar bort en produkt ur kundvagnen. Körs endast när den trycker på nedåtpilen i kundvagnen. Tanken var att kunna ta bort mer än en
        /// produkt, och funktionaliteten finns, men gränssnittet stödjer det inte för tillfället. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amt"></param>
        /// <returns></returns>
        public IActionResult OnGetRemove(int id, int amt = 1)
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                string host = HttpContext.Request.Headers["Referer"];
                return Redirect(host);
            }
            else
            {
                // Hämtar nuvarande "inloggad" användare och dess kundvagn.
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                SessionInfo_Cart = HttpContext.Session.Get<CartDTO>(SessionKeyCart);
                if (SessionInfo_Cart.Products.ContainsKey(id))
                {
                    int amount = 0;
                    SessionInfo_Cart.Products[id] -= amt; // Ta bort en ifrån mängden produkter som finns sparad.
                    amount = SessionInfo_Cart.Products[id];
                    if (SessionInfo_Cart.Products[id] == 0) // Om mängden produkter slutar på 0, ta bort produkten helt ur Dictionary-objektet.
                    {
                        SessionInfo_Cart.Products.Remove(id);
                    }
                    _cartAccess.Update(SessionInfo_Cart); // Uppdatera kundvagnen i systemet.
                    HttpContext.Session.Set<CartDTO>(SessionKeyCart, SessionInfo_Cart); // Skjut in nuvarande version av kundvagnen i sessionen.
                    string result = "{ \"amount\" : " + amount + " }";
                    return Content(result, "application/json");
                }
                else throw new KeyNotFoundException("Could not find the product.");
            }
        }
    }
}
