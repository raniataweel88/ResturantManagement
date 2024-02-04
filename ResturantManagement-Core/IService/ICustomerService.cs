using Microsoft.AspNetCore.Mvc;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
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
        Task<CustomerDTO> GetCustomerByIdAsync(int Id);
        Task CreateCustomer(CustomerDTO c);
        Task UpdateCustomer(CustomerDTO c);
        Task DeleteCustomer(int Id);
    }
}
