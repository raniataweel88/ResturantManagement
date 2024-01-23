using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.DTO
{
    public class MenuDTO
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        
    }
}
