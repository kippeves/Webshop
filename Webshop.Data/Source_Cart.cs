using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Webshop.DTO;

namespace Webshop.DataSource
{
    public class Source_Cart : DataSource_JSON<CartDTO>
    {
        private static string _PATH = AppDomain.CurrentDomain.BaseDirectory + "carts.json";

        public Source_Cart() : base(_PATH)
        {
        }
    }
}
