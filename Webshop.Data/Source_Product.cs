using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.DTO;

namespace Webshop.DataSource
{
   public class Source_Products: DataSource_JSON<ProductDTO>
    {
        private static string _PATH = AppDomain.CurrentDomain.BaseDirectory + "products.json";

        public Source_Products() : base(_PATH)
        {
        }
    }
}
