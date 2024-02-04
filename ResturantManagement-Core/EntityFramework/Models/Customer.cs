using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string? AccessKey { get; set; }
       public List<Order> Order { get; set; }
        public DateTime? AccesskeyExpireDate { get; set; }
        public bool IsLoggedIn {get;set;}
    }
}
