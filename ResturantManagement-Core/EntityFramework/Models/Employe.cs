using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Models
{
    public class Employe
    {
        public int EmployeId { get; set; }
        public string Name { get; set; }
        public string? Position { get; set; }
    }
}
