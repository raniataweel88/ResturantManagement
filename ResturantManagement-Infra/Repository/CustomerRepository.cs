using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.IRepository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Infra.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public CustomerRepository(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        #region Customer
        public async Task CreateCustomer(Customer c)
        {
            Log.Debug("Debugging Create Customer Repository has been started");
            Customer cs = new Customer();
            cs.Email= c.Email;
            cs.Name = c.Name;
            cs.Phone = c.Phone;
            await _context.AddAsync(cs);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new customer Repository");
            Log.Debug("Debugging Create Customer Repository been finished");
        }

        public async Task DeleteCustomer(int Id)
        {
            Log.Debug($"Debugging DeleteCustomer Repository has been started");
            var result = _context.Customers.FindAsync(Id);
            if (result != null)
            {
                Log.Information("Customer Is exist");
                _context.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes the customer successfully Repository");
                Log.Debug($"Debugging DeleteCustomer Repository has been finished");
            }
            Log.Error("Question Type Not Found");
        }

        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            Log.Debug("Debugging GetAllCustomerAsync Repository has been started ");
            var Customer = await _context.Customers.ToListAsync();
            var result = from c in Customer
                         select new Customer
                         {
                             Name = c.Name,
                             CustomerId = c.CustomerId,
                             Phone = c.Phone,
                         };
            Log.Information("Db query has been Get All Customers Repository");
            Log.Debug("Debugging GetAllCustomerAsync Repository has been finished");
            return (result.ToList());
        }

        public async Task GetCustomerById(int Id)
        {
            Log.Debug("Debugging GetCustomerById Repository has been started");
            var result = await _context.Customers.AnyAsync(x => x.CustomerId == Id);
            Log.Information($"Db Query has been get Customers Id");
            Log.Debug($"Debugging GetCustomerById Repository Has been Finished Successfully With CustomerId ");
        }
        public async Task UpdateCustomer(Customer c)
        {
            Log.Debug($"Debugging UpdateCustomere Repository has been started");
            var result = await _context.Customers.FindAsync(c.CustomerId);
            result.Name = c.Name;
            result.Email = c.Email; 
            result.Phone = c.Phone;
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates customer Repository");
            Log.Debug($"Debugging UpdateCustomere Repository has been Finished"); ;
        }
        #endregion

    }
}

