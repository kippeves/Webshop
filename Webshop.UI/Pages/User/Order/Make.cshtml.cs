using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User.Order
{
    public class MakeModel : PageModel
    {
        readonly DAL_Order _orderAccess;
        readonly DAL_Card _cardAccess;
        readonly DAL_Cart _cartAccess;
        public List<CardDTO> Cards { get; set; }
        
        public const string SessionKeyCustomer  = "_Customer";
        public const string SessionKeyCart      = "_Cart";
        
        //Nuvarande inloggande kund
        [BindProperty]
        public CustomerDTO SessionInfo_Customer { get; private set; }
        [BindProperty]
        public ReceiptDTO ReceiptModel { get; set; }
        public int OrderId {get;set;}
        public string ErrorMessage { get; set; }
        public MakeModel(DAL_Order orderAccess, DAL_Card cardAccess, DAL_Cart cartAccess)
        {
            _orderAccess    = orderAccess;
            _cardAccess     = cardAccess;
            _cartAccess     = cartAccess;
        }


        /// <summary>
        /// Körs när en användare vill "slutföra" en order. Innehåller 
        /// kortinformation och textfältet för en kunds lösenord.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OnGet(int? id) {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            // Om det inte finns en kund "inloggad"
            if (default == SessionInfo_Customer)
            {
                // Skicka tillbaka till index.
                return RedirectToPage("/Index");
            }
            else {
                // Om det finns en användare och det finns ett ID-värde ifyllt som variabel för adressen.
                if (id.HasValue)
                {
                    // Kolla vilka kort som finns för kund
                    Cards = _cardAccess.FindByCustomer(SessionInfo_Customer.Id).ToList();
                    OrderId = id.Value;
                    return Page();
                } else return RedirectToPage("/User/Index");
            }
        }

        /// <summary>
        /// När en användare vill lägga en order. Tar bort den nuvarande kundvagnen och skapar en order.
        /// Användaren skickas sen vidare till listan över ordrar, där den kan välja vad den vill göra med den.
        /// </summary>
        /// <param name="cart">Vilken kundvagn som den vill göra en order av. Kund-ID kommer alltid vara den kund som är inloggad.</param>
        /// <returns></returns>

        public ActionResult OnGetPut(int cart) {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            if (default == SessionInfo_Customer)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                CartDTO c = _cartAccess.LoadById(cart);
                int orderNo = _orderAccess.PutOrder(SessionInfo_Customer,c);
                _cartAccess.Delete(c);
                HttpContext.Session.Set<CartDTO>(SessionKeyCart, new(SessionInfo_Customer.Id));
                return RedirectToPage("/User/Order/Index", new { id = orderNo });
            }
        }
    }
}
