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
  public class Source_Receipt : DataSource_JSON<ReceiptDTO>
    {
        private static string _PATH = AppDomain.CurrentDomain.BaseDirectory + "receipts.json";

        public Source_Receipt() : base(_PATH)
        {
        }
    }
}
