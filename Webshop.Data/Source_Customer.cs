using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Webshop.DTO;

namespace Webshop.DataSource
{
    public class Source_Customer : DataSource_JSON<CustomerDTO>
    {
        private static string _PATH = AppDomain.CurrentDomain.BaseDirectory + "customers.json";

        public Source_Customer() : base(_PATH)
        {
        }
    }
}