using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.User
{
    public class CardModel : PageModel
    {
        readonly DAL_Card _cardAccess;
        public const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }
        public List<CardDTO> Cards { get; set; }
        public CardModel(DAL_Card cardAccess)
        {
            _cardAccess = cardAccess;
        }

        public ActionResult OnGet()
        {
            CustomerDTO SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            if (default == SessionInfo_Customer)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                Cards = _cardAccess.FindByCustomer(SessionInfo_Customer.Id).ToList();
                return Page();
            }
        }
    }
}
