using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Webshop.DAL;
using Webshop.DTO;

namespace Webshop.UI.Pages.Cart
{
    public class IndexModel : PageModel
    {
        readonly IDataAccess<CartDTO> _carts;
        readonly IDataAccess<ProductDTO> _products;

        public Dictionary<ProductDTO,int> Products { get; set; }

        public IndexModel(IDataAccess<CartDTO> carts, IDataAccess<ProductDTO> products)
        {
            _carts = carts;
            _products = products;
        }

        public void OnGet()
        {
            Products = new();
            CartDTO userCart = _carts.LoadById(1);
            foreach (var item in userCart.products)
            { 
                Products.Add(_products.LoadById(item.Key),item.Value);
            }
        }

        public ActionResult OnPostAdd() {

            return Page();
        }
    }
}
