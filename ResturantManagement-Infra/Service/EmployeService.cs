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
    public class EmployeService : IEmployeService
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public EmployeService(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        #region Employe
        public async Task CreateEmploye(EmployeDTO dto)
        {
            Log.Debug("Debugging CreateEmploye Service has been started");
            Employe e = new Employe();
            e.Name = dto.Name;
            await _context.AddAsync(e);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new Employe Service");
            Log.Debug("Debugging CreateEmploye Service been finised");
        }

        public async Task DeleteEmploye(int Id)
        {
            Log.Debug($"Debugging DeleteEmploye Service has been started");
            var result = _context.Employes.FindAsync(Id);
            if (result != null)
            {
                Log.Information("Employe  Is Exist");
                _context.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes the item of Employe Service successfully");
                Log.Debug($"Debugging DeleteEmploye Service has been finished");
            }
            Log.Error("Employe Not Found");
        }

        public async Task<List<EmployeDTO>> GetAllEmployeAsync()
        {
            Log.Debug("Debugging GetAllEmployeAsync Service has been started");
            var Employe = await _context.Employes.ToListAsync();
            var result = from e in Employe
                         select new EmployeDTO
                         {
                             Name = e.Name,
                             EmployeId = e.EmployeId,
                         };
            Log.Information("Db query has been Get All Employe Service");
            Log.Debug("Debugging GetAllEmployeAsync Service has been finised");
            return (result.ToList());
        }

        public async Task GetEmployeById(int Id)
        {
            Log.Debug("Debugging GetEmployeById Service has been started");
            var result = await _context.Employes.AnyAsync(x => x.EmployeId == Id);
            Log.Information($"Db Query has been get Employe Id Service");
            Log.Debug($"Debugging GetEmployeById Service Has been Finished Successfully With CustomerId");
        }
        public async Task UpdateEmploye(EmployeDTO dto)
        {
            Log.Debug($"Debugging UpdateEmploy Service has been started");
            var result = await _context.Employes.FindAsync(dto.EmployeId);
            result.Name = dto.Name;
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Service");
            Log.Debug($"Debugging UpdateEmploy Service has been Finshied"); ;
        }
    }
}
#endregion
