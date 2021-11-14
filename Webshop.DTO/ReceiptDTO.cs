using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DTO
{
    public class ReceiptDTO
    {
        public int id { get; set; }
        public int CardId { get; set; }
        public int OrderId { get; set; }
        public DateTime date_paid { get; set; }
    }
}
