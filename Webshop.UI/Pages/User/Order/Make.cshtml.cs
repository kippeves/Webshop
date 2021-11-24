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
        /// K�rs n�r en anv�ndare vill "slutf�ra" en order. Inneh�ller 
        /// kortinformation och textf�ltet f�r en kunds l�senord.
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
                // Om det finns en anv�ndare och det finns ett ID-v�rde ifyllt som variabel f�r adressen.
                if (id.HasValue)
                {
                    // Kolla vilka kort som finns f�r kund
                    Cards = _cardAccess.FindByCustomer(SessionInfo_Customer.Id).ToList();
                    OrderId = id.Value;
                    return Page();
                } else return RedirectToPage("/User/Index");
            }
        }

        /// <summary>
        /// N�r en anv�ndare vill l�gga en order. Tar bort den nuvarande kundvagnen och skapar en order.
        /// Anv�ndaren skickas sen vidare till listan �ver ordrar, d�r den kan v�lja vad den vill g�ra med den.
        /// </summary>
        /// <param name="cart">Vilken kundvagn som den vill g�ra en order av. Kund-ID kommer alltid vara den kund som �r inloggad.</param>
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
