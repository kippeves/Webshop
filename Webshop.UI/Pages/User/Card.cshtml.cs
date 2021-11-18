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
        readonly DAL_Receipt _receiptAccess;
        readonly DAL_Order _orderAccess;
        public const string SessionKeyCustomer = "_Customer";
        public CustomerDTO SessionInfo_Customer { get; private set; }
        public List<CardDTO> Cards { get; set; }
        public CardModel(DAL_Card cardAccess, DAL_Receipt receiptAccess, DAL_Order orderAccess)
        {
            _cardAccess = cardAccess;
            _receiptAccess = receiptAccess;
            _orderAccess = orderAccess;

        }
        [BindProperty]
        public string PassString { get; set; }
        [BindProperty]
        public int CardId { get; set; }
        [BindProperty]
        public int OrderId { get; set; }

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

        public ActionResult OnPostPay(){
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else {
                CustomerDTO SessionInfo_Customer = HttpContext.Session.Get<CustomerDTO>(SessionKeyCustomer);
                string ComputerHash;
                using (SHA256 hash = SHA256Managed.Create())
                {
                    ComputerHash = String.Concat(hash
                      .ComputeHash(Encoding.UTF8.GetBytes(PassString))
                      .Select(item => item.ToString("x2")));
                }

                if (ComputerHash == SessionInfo_Customer.Hash)
                {
                    _orderAccess.Pay(OrderId,SessionInfo_Customer.Id);
                    ReceiptDTO receipt = new (CardId, OrderId);
                    _receiptAccess.Save(receipt);
                    return RedirectToPage("/User/Order/Index");
                }
                else {
                    TempData["ErrorMessage"] = "Du skrev in fel kod. Vänligen försök igen.";
                    return RedirectToPage("/User/Order/Make", new { id = OrderId });
                }
            }
        }
    }
}
