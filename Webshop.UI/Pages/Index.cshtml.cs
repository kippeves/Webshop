using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages
{
    public class IndexModel : PageModel
    {
        readonly   DAL_Product _productAccess;
        public     List<ProductDTO> products;
        public CustomerDTO SessionInfo_Customer { get; private set; }
        public const string SessionKeyCustomer = "_Customer";

        public IndexModel(DAL_Product productAccess)
        {
            _productAccess = productAccess;
        }



        // Visas alla produkter på sidan.
        public IActionResult OnGet()
        {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            if (SessionInfo_Customer != default) { // Om det finns en användare "inloggad"
                ViewData["username"] = SessionInfo_Customer.Name; // Visa namnet i vyn. (Visas i kundmenyn)
            }
            products = _productAccess.LoadAll().ToList(); // Visa alla produkter oavsett.
            return Page();
        }

        /// <summary>
        /// Söker efter en produkt på sidan.
        /// </summary>
        /// <param name="term">Namnet på produkten som skall hittas.</param>
        /// <returns></returns>
        public IActionResult OnPostSearch(string term) 
        {
            if (!string.IsNullOrEmpty(term))
            {
                //Hämtar alla produkter vars namn "stämmer överens" med det sökna namnet.
                // Kör små bokstäver för att endast söka på värde istället för små och stora bokstäver.
                products = _productAccess.LoadAll().Where(p => p.Name.ToLower().Contains(term.ToLower())).ToList();
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                return Page();
            }
            return RedirectToPage("/Index");
        }

        /// <summary>
        /// Sorterar produkten på sidan efter pris.
        /// </summary>
        /// <param name="sortTerm">Bestämmer hur resultet ska sorteras. Asc på det skall vara högst först, desc om det ska vara minst först.</param>
        /// <returns></returns>
        public IActionResult OnPostSort(string sortTerm)
        {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            if (sortTerm == "asc")
            {
                products = _productAccess.LoadAll().OrderBy(p => p.Price).ToList();
            }
            else products = _productAccess.LoadAll().OrderByDescending(p => p.Price).ToList();
            return Page();
        }
    }
}
