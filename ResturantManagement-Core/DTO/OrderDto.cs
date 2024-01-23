using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
       

        public decimal TotalPrice { get; set; }
    }
}
