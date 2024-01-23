using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal Price { get; set; }
    }
}
