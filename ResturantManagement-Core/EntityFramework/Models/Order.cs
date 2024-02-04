using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Models
{
    public class Order
    {
        public int OrderId { get; set; }   
        public Table? Table { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public  Customer Customer { get; set; }
        public Employe Employe { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderItemId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeId { get; set; }
    }
}
