using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.DTO
{
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }
      
    }
}
