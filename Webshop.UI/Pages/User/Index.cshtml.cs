using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User
{
    public class IndexModel : PageModel
    {
        readonly DAL_Customer   _customerAccess;
        readonly DAL_Cart       _cartAccess;
        public const string SessionKeyCustomer  = "_Customer";
        public const string SessionKeyCart      = "_Cart";
        public CustomerDTO  SessionInfo_Customer { get; private set; }
        public CartDTO      SessionInfo_Cart { get; private set; }


        public IndexModel(DAL_Customer customerAccess,DAL_Cart cartAccess)
        {
            _customerAccess = customerAccess;
            _cartAccess     = cartAccess;
        }
        /// <summary>
        /// Visar information om en kund. Har ingen funktionalitet.
        /// </summary>
        /// <returns></returns>
        public ActionResult OnGet()
        {
            if (HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer) == default)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                ViewData["username"] = SessionInfo_Customer.Name;
                return Page();
            }
        }
    
        /// <summary>
        /// Loggar in en användare.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OnPostSet(int id) {
            // Sätter en kund. Har absolut ingen felhantering, eftersom det är hårdkodat vilka kunder som finns.
            // Jag hade INTE gjort så här om jag hade haft en inloggningsskärm.
            HttpContext.Session.Set<CustomerDTO>(SessionKeyCustomer, _customerAccess.LoadById(id));
            CartDTO tempCart = _cartAccess.LoadById(id); // Hämtar ut en kunds kundvagn.
            if(tempCart == default){ // Om inte kunden har någon, så är objektet Default (tekniskt sett samma sak som Null), så kontroll görs på Default. 
                tempCart = new CartDTO(id); // Skapa en ny kundvagn och lagra den.
                _cartAccess.Save(tempCart);
            }
            HttpContext.Session.Set<CartDTO>(SessionKeyCart, tempCart); // Sätt kundvagnen i sessionen.
            return RedirectToPage("/Index");
        }
    }
}
