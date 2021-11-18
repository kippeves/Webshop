using System;
using Webshop.DTO;

namespace Webshop.DataSource
{
    public class Source_Order : DataSource_JSON<OrderDTO>
    {
        private static string _PATH = AppDomain.CurrentDomain.BaseDirectory + "orders.json";
        public Source_Order(string PATH) : base(PATH)
        {

        }
    }
}
