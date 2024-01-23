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
    public class TableRepository : ITableRepository
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public TableRepository(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        #region Table
        public async Task CreateTable(Table ta)
        {
            Log.Debug("Debugging Create Table Repository has been started");
            Table t = new Table();
            t.TableNumber = ta.TableNumber;
            await _context.AddAsync(t);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new Table Repository");
            Log.Debug("Debugging Create Table Repository has been finisded");
        }

        public async Task DeleteTable(int Id)
        {
            Log.Debug($"Debugging DeleteTable Repository has been started");
            var result = _context.Tables.FindAsync(Id);
            if (result != null)
            {
                Log.Information("Table Is Exist");
                _context.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes the Table Repository successfully");
                Log.Debug($"Debugging DeleteTable Repository has been finished");
            }
            Log.Error("Table Not Found");
        }

        public async Task<List<Table>> GetAllTableAsync()
        {
            Log.Debug("Debugging GetAllTableAsync Repository has been started");
            var Table = await _context.Tables.ToListAsync();
            var result = from t in Table
                         select new Table
                         {
                             TableNumber = t.TableNumber,
                         };
            Log.Information("Db query has been Get All Table Repository");
            Log.Debug("Debugging GetAllTableAsync Repository has been finised");
            return (result.ToList());
        }

        public async Task GetTableById(int Id)
        {
            Log.Debug("Debugging GetTableById  Repository has been started");
            var result = await _context.Tables.AnyAsync(x => x.TableId == Id);
            Log.Information($"Db Query has been get Table Id Repository");
            Log.Debug($"Debugging GetMenuById Repository Has been Finished Successfully With TableId ");
        }
        public async Task UpdateTable(Table t)
        {
            Log.Debug($"Debugging UpdateTable Repository has been started");
            var result = await _context.Tables.FindAsync(t.TableId);
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Repository");
            Log.Debug($"Debugging UpdateTable Repository has been Finished"); 
        }
        #endregion
    }
}