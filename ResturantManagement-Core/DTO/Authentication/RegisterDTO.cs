using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.DTO.Authentication
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string name { get; set; }
        public string TypeOfUser { get; set; }
        public string PhoneNumber { get; set; }
    }
}
