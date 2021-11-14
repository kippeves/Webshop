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
    public class IndexModel : PageModel
    {
        [ViewData]
        public string Title { get; set; }
        readonly IDataAccess<CustomerDTO> _dataAccess;
        public List<CustomerDTO> customers;

        public IndexModel(IDataAccess<CustomerDTO> dataAccess)
        {
            _dataAccess = dataAccess;
            customers = _dataAccess.LoadAll().ToList();
            Title = "Index";
        }
        public void OnGet()
        {
        }
    }
}
