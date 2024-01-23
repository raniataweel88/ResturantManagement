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
    public class TableService : ITableService
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public TableService(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        #region Table
        public async Task CreateTable(TableDto dto)
        {
            Log.Debug("Debugging Create Table Service has been started");
            Table t = new Table();
            t.TableNumber = dto.TableNumber;
            await _context.AddAsync(t);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new Table Service");
            Log.Debug("Debugging Create Table Service has been finised");
        }

        public async Task DeleteTable(int Id)
        {
            Log.Debug($"Debugging DeleteTable Service has been started");
            var result = _context.Tables.FindAsync(Id);
            if (result != null)
            {
                Log.Information("Table Is Exist");
                _context.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes the Table Service successfully");
                Log.Debug($"Debugging DeleteTable Service has been finished");
            }
            Log.Error("Table Not Found");
        }

        public async Task<List<TableDto>> GetAllTableAsync()
        {
            Log.Debug("Debugging GetAllTableAsync Service has been started");
            var Table = await _context.Tables.ToListAsync();
            var result = from t in Table
                         select new TableDto
                         {
                             TableNumber = t.TableNumber,
                         };
            Log.Information("Db query has been Get All Table Service");
            Log.Debug("Debugging GetAllTableAsync Service has been finised");
            return (result.ToList());
        }

        public async Task GetTableById(int Id)
        {
            Log.Debug("Debugging GetTableById  Service has been started");
            var result = await _context.Tables.AnyAsync(x => x.TableId == Id);
            Log.Information($"Db Query has been get Table Id Service");
            Log.Debug($"Debugging GetMenuById Service Has been Finished Successfully With TableId ");
        }
        public async Task UpdateTable(TableDto dto)
        {
            Log.Debug($"Debugging UpdateTable Service has been started");
            var result = await _context.Tables.FindAsync(dto.TableId);
           result.TableNumber=dto.TableNumber;
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Service");
            Log.Debug($"Debugging UpdateTable Service has been Finished"); ;
        }
        #endregion
    }
}