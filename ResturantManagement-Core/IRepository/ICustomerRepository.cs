using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IRepository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomerAsync();
        Task GetCustomerById(int Id);
        Task CreateCustomer(Customer c);
        Task UpdateCustomer(Customer c);
        Task DeleteCustomer(int Id);
    }
}
