using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public Menu Menu { get; set; }
        public Order Order { get; set; }
        public int MenuId {  get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}

