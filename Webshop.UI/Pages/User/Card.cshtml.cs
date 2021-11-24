using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        readonly DAL_Receipt _recieptAccess;
        readonly DAL_Order _orderAccess;
        public const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }
        public List<CardDTO> Cards { get; set; }
        public ReceiptDTO ReceiptModel { get; set; }
        [BindProperty]
        public string PassString { get; set; }
        [BindProperty]
        public int CardId { get; set; }
        [BindProperty]
        public int OrderId { get; set; }
        public CardModel(DAL_Card cardAccess, DAL_Receipt recieptAccess, DAL_Order orderAccess)
        {
            _cardAccess = cardAccess;
            _recieptAccess = recieptAccess;
            _orderAccess = orderAccess;
        }

        //Visar information om en kunds kort.
        public ActionResult OnGet()
        {
            CustomerDTO SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
            ViewData["username"] = SessionInfo_Customer.Name;
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

        /// <summary>
        /// K�rs n�r en kund vill betala en order.
        /// </summary>
        /// <param name="ReceiptModel">Tar emot ett Kvitto-objekt. Vid det h�r lagret har all information som kr�vs f�r att modellen skall vara komplett
        /// redan f�rts in av order-sidan. All information kommer d�rf�r att f�ras in systemet. </param>
        /// <returns></returns>
        public ActionResult OnPostPay(ReceiptDTO ReceiptModel){
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else {
                // H�mtar nuvarande "inloggad" kund.
                CustomerDTO SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                string ComputerHash;
                using (SHA256 hash = SHA256Managed.Create())
                {
                    // Hashar inskriven l�senstr�ng,
                    ComputerHash = String.Concat(hash
                      .ComputeHash(Encoding.UTF8.GetBytes(PassString))
                      .Select(item => item.ToString("x2"))); 
                }
                // J�mf�r med lagrad l�sen str�ng
                if (ComputerHash == SessionInfo_Customer.Hash)
                {
                    // Om det st�mmer, s� flippa bool:en i ordern till betalad och spara i kvittosystemet.
                    _orderAccess.Pay(ReceiptModel.OrderId, ReceiptModel.CustomerId);
                    _recieptAccess.Save(ReceiptModel);
                    return RedirectToPage("/User/Order/Index");
                }
                else { // Annars skicka tillbaka felmeddelande.
                    TempData["ErrorMessage"] = "Du skrev in fel kod. V�nligen f�rs�k igen.";
                    return RedirectToPage("/User/Order/Make", new { id = OrderId });
                }
            }
        }
    }
}
