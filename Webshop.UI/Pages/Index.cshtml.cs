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
        readonly IDataAccess<ProductDTO> _dataAccess;
        public List<ProductDTO> products;

        public IndexModel(IDataAccess<ProductDTO> dataAccess)
        {
            _dataAccess = dataAccess;
            products= _dataAccess.LoadAll().ToList();
        }

        public void OnGet()
        {
        }
    }
}
