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



        // Visas alla produkter p� sidan.
        public IActionResult OnGet()
        {
            SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            if (SessionInfo_Customer != default) { // Om det finns en anv�ndare "inloggad"
                ViewData["username"] = SessionInfo_Customer.Name; // Visa namnet i vyn. (Visas i kundmenyn)
            }
            products = _productAccess.LoadAll().ToList(); // Visa alla produkter oavsett.
            return Page();
        }

        /// <summary>
        /// S�ker efter en produkt p� sidan.
        /// </summary>
        /// <param name="term">Namnet p� produkten som skall hittas.</param>
        /// <returns></returns>
        public IActionResult OnPostSearch(string term) 
        {
            if (!string.IsNullOrEmpty(term))
            {
                //H�mtar alla produkter vars namn "st�mmer �verens" med det s�kna namnet.
                // K�r sm� bokst�ver f�r att endast s�ka p� v�rde ist�llet f�r sm� och stora bokst�ver.
                products = _productAccess.LoadAll().Where(p => p.Name.ToLower().Contains(term.ToLower())).ToList();
                SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                return Page();
            }
            return RedirectToPage("/Index");
        }

        /// <summary>
        /// Sorterar produkten p� sidan efter pris.
        /// </summary>
        /// <param name="sortTerm">Best�mmer hur resultet ska sorteras. Asc p� det skall vara h�gst f�rst, desc om det ska vara minst f�rst.</param>
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
