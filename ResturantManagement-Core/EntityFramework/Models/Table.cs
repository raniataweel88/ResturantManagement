using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Models
{
    public class Table
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public List<Order> Orders { get; set; }
    }
}
