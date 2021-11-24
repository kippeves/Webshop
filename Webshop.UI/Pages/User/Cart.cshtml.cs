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
            // Finns ingen kund inloggad, s� skicka tillbaka till index.
            if (SessionInfo_Customer == default)
            {
                RedirectToPage("/Index");
            }
            else { // H�mta lagrad kund och kundvagn
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                SessionInfo_Cart = HttpContext.Session.Get<CartDTO>(SessionKeyCart);
                foreach (var item in SessionInfo_Cart.Products)
                { // Skapa en lista med produkter, baserat p� kundvagnen.
                    Products.Add(_productAccess.LoadById(item.Key), item.Value);
                }
            }
        }
    

        /// <summary>
        /// L�gger till en produkt i kundvagnen. K�rs n�r en kund trycker p� 1. L�gg till produkt, eller 2. N�r den trycker p� upp�tpilen i kundvagnen.
        /// Tanken var att l�gga till en valfri m�ngd produkter att kunna �ka med, men jag lade aldrig till det i gr�nssnittet.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amt"></param>
        /// <returns>Ett JSonResult-objekt inneh�llande {"amount": m�ngden som produkten hade n�r den var uppdaterad}</returns>
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
        /// Tar bort en produkt ur kundvagnen. K�rs endast n�r den trycker p� ned�tpilen i kundvagnen. Tanken var att kunna ta bort mer �n en
        /// produkt, och funktionaliteten finns, men gr�nssnittet st�djer det inte f�r tillf�llet. 
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
                // H�mtar nuvarande "inloggad" anv�ndare och dess kundvagn.
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                SessionInfo_Cart = HttpContext.Session.Get<CartDTO>(SessionKeyCart);
                if (SessionInfo_Cart.Products.ContainsKey(id))
                {
                    int amount = 0;
                    SessionInfo_Cart.Products[id] -= amt; // Ta bort en ifr�n m�ngden produkter som finns sparad.
                    amount = SessionInfo_Cart.Products[id];
                    if (SessionInfo_Cart.Products[id] == 0) // Om m�ngden produkter slutar p� 0, ta bort produkten helt ur Dictionary-objektet.
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
