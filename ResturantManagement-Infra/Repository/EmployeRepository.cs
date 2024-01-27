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
    public class EmployeRepository : IEmployeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public EmployeRepository(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        #region Employee
        public async Task CreateEmploye(Employe e)
        {
            Log.Debug("Debugging CreateEmploye Repository has been started");
            Employe em = new Employe();
            try { em.Name=e.Name;
            await _context.AddAsync(em);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new Employe Repository") ;}
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "can not add new Employe");
            }
            Log.Debug("Debugging CreateEmploye been finished Repository");
        }

        public async Task DeleteEmploye(int Id)
        {
            Log.Debug($"Debugging DeleteEmploye Repository has been started");
           try{ var result = _context.Employes.FindAsync(Id);
                if (result != null)
                {
                    Log.Information("Employe  Is exist");
                    _context.Remove(result);
                    await _context.SaveChangesAsync();
                    Log.Information("Db Query delets the Employe Repository successfully");
                } 
            }
            catch (Exception ex)
            {  Log.Error("Employe Not Found");
                throw new Exception(ex.Message + "Employe Not Found");
            }
               Log.Debug($"Debugging DeleteEmploye  Repository has been finished");

        }

        public async Task<List<Employe>> GetAllEmployeAsync()
        {
            Log.Debug("Debugging GetAllEmployeeAsync Repository has been started");
            try {   var Employe = await _context.Employes.ToListAsync();
            var result = from e in Employe
                         select new Employe
                         {
                             Name = e.Name,
                            EmployeId=e.EmployeId,
                         };
            Log.Information("Db query has been Get All Employee Repository");       
                return (result.ToList());}
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "the model Employee is empty");
            }
            Log.Debug("Debugging GetAllEmployeeAsync Repository has been finished");
   
        }

        public async Task GetEmployeById(int Id)
        {
                Log.Debug("Debugging GetEmployeeById Repository has been started");
            try { 
                var result = await _context.Employes.AnyAsync(x => x.EmployeId == Id);
            Log.Information($"Db Query has been get Employee Id Repository"); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "the object of model Employee is empty");
            }
            Log.Debug($"Debugging GetEmployeeById Repository Has been Finished Successfully With CustomerId");
        }
        public async Task UpdateEmploye(Employe e)
        {
            Log.Debug($"Debugging UpdateEmploy Repository has been started");
            try {     var result = await _context.Employes.FindAsync(e.EmployeId);     
                result.Name=e.Name;
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Employ Repository");}
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "the object of model Employee is empty ");
            }

            Log.Debug($"Debugging UpdateEmploy Repository has been Finished"); ;
        }
        #endregion
    }
}

