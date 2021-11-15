using System.ComponentModel.DataAnnotations.Schema;

namespace Webshop.DTO
{
    public class CardDTO
    {
        public int CustomerRefID { get; set; }
        public int NameOnCard { get; set; }
        public int CardNo { get; set; }
        public int CVS { get; set; }
    }
}
