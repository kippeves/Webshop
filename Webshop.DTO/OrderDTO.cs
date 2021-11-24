using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Dictionary<int,int> Content{ get; set; }
        public bool Is_paid { get; set; }
        public OrderDTO(int id, int customer, Dictionary<int,int> content)
        {
            Id = id;
            CustomerId = customer;
            Content = content;
            Is_paid = false;
        }
    }
}
