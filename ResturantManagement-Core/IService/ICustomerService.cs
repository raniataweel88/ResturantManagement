using ResturantManagement_Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IService
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomerAsync();
        Task GetCustomerById(int Id);
        Task CreateCustomer(CustomerDTO c);
        Task UpdateCustomer(CustomerDTO c);
        Task DeleteCustomer(int Id);
    }
}
