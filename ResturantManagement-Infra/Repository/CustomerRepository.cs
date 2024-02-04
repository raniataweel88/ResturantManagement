using Microsoft.AspNetCore.Mvc;
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
            try {  
            cs.Email= c.Email;
            cs.Name = c.Name;
            cs.Phone = c.Phone;
            await _context.AddAsync(cs);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new customer Repository");
            }
            catch(Exception ex)
            {
                throw new Exception(" does not add new customer");
            }
           
            Log.Debug("Debugging Create Customer Repository been finished");
        }

        public async Task DeleteCustomer(int Id)
        {     Log.Debug($"Debugging DeleteCustomer Repository has been started");
            try
            {
                var result = _context.Customers.FindAsync(Id);
                if (result != null)
                {
                    Log.Information("Customer Is exist");
                    _context.Remove(result);
                    await _context.SaveChangesAsync();
                    Log.Information("Db Query deletes the customer successfully Repository");
                }
            }
            catch (Exception ex)
            {
             Log.Error("Customer Not Found");
                throw new Exception($"Customer Not Found");
            }
            Log.Debug($"Debugging DeleteCustomer Repository has been finished");
        }

        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            Log.Debug("Debugging GetAllCustomerAsync Repository has been started ");
            try
            {
                var Customer = await _context.Customers.ToListAsync();
                var result = from c in Customer
                             select new Customer
                             {
                                 Name = c.Name,
                                 CustomerId = c.CustomerId,
                                 Phone = c.Phone,
                             };
                Log.Information("Db query has been Get All Customers Repository");
                return (result.ToList());
            }   
            catch (Exception ex) {
                throw new Exception(ex.Message+ "Does not have any object in model customer");
            }
            Log.Debug("Debugging GetAllCustomerAsync Repository has been finished");

        }

        public async Task<Customer> GetCustomerById(int Id)
        {
            try
            {
                Log.Debug("Debugging GetCustomerById Service has been started");
                return await _context.Customers.FindAsync(Id);
                Log.Information($"Db Query has been get Customers Id Service");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            Log.Debug($"Debugging GetCustomerById Service Has been Finished Successfully With CustomerId");

        }
    
        public async Task UpdateCustomer(Customer c)
        {
            Log.Debug($"Debugging UpdateCustomere Repository has been started");
            try {
            var result = await _context.Customers.FindAsync(c.CustomerId);
            result.Name = c.Name;
            result.Email = c.Email; 
            result.Phone = c.Phone;
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates customer Repository"); }
             catch (Exception ex) 
            {
                throw new Exception(ex.Message + "Db query the object of model Customers is empty");
            }
            Log.Debug($"Debugging Update Customer Repository has been Finished"); ;
        }
        #endregion

    }
}

