using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.IRepository;
using ResturantManagement_Core.IService;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Infra.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public CustomerService(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        #region Customer
        public async Task CreateCustomer(CustomerDTO dto)
        {
            Log.Debug("Debugging Create Customer Service has been started");
            Customer c = new Customer();
            c.Name = dto.Name;
            c.Phone = dto.Phone;
            c.Email = dto.Email;
            await _context.AddAsync(c);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new customer Service");
            Log.Debug("Debugging Create Customer Service been finished");
        }

        public async Task DeleteCustomer(int Id)
        {
            Log.Debug($"Debugging DeleteCustomer Service has been started");
            var result = _context.Customers.FindAsync(Id);
            if (result != null)
            {
                Log.Information("Customer Is exist");
                _context.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes the customer Service successfully");
                Log.Debug($"Debugging DeleteCustomer Service has been finished");
            }
            Log.Error("Question Type Not Found");
        }

        public async Task<List<CustomerDTO>> GetAllCustomerAsync()
        {
            Log.Debug("Debugging GetAllCustomerAsync Service has been started");
            var Customer = await _context.Customers.ToListAsync();
            var result = from c in Customer
                         select new CustomerDTO
                         {
                             Name = c.Name,
                             CustomerId = c.CustomerId,
                             Phone = c.Phone,
                         };
            Log.Information("Db query has been Get All Customers Service");
            Log.Debug("Debugging GetAllCustomerAsync Service has been finished");
            return (result.ToList());
        }

        public async Task GetCustomerById(int Id)
        {
            Log.Debug("Debugging GetCustomerById Service has been started");
            var result = await _context.Customers.AnyAsync(x => x.CustomerId == Id);
            Log.Information($"Db Query has been get Customers Id Service");
            Log.Debug($"Debugging GetCustomerById Service Has been Finished Successfully With CustomerId");
        }
        public async Task UpdateCustomer(CustomerDTO dto)
        {
            Log.Debug($"Debugging UpdateCustomere Service has been started Service");
            var result = await _context.Customers.FindAsync(dto.CustomerId);
            result.Name = dto.Name;
            result.Phone = dto.Phone;
            result.Email = dto.Email;
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Service");
            Log.Debug($"Debugging UpdateCustomere Service has been Finished"); ;
        }

        #endregion

    }
}
