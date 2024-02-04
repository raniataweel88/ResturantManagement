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
            try
            {
            Table t = new Table();
            t.TableNumber = ta.TableNumber;
            await _context.AddAsync(t);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new Table Repository");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Db can not  add new Table");
            }

            Log.Debug("Debugging Create Table Repository has been finisded");
        }

        public async Task DeleteTable(int Id)
        {
            Log.Debug($"Debugging DeleteTable Repository has been started");
            try {        var result = _context.Tables.FindAsync(Id);
            if (result != null)
            {
                Log.Information("Table Is Exist");
                _context.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes the Table Repository successfully"); }  }
             catch (Exception ex)
            {   Log.Error("Table Not Found");
                throw new Exception(ex.Message + "Table Not Found");
            }
            Log.Debug($"Debugging DeleteTable Repository has been finished");     
        }
        public async Task<List<Table>> GetAllTableAsync()
        {
            Log.Debug("Debugging GetAllTableAsync Repository has been started");
            try
            {
        var Table = await _context.Tables.ToListAsync();
            var result = from t in Table
                         select new Table
                         {
                             TableNumber = t.TableNumber,
                         };
            Log.Information("Db query has been Get All Table Repository");   
                return (result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Db query do not have objects in model");
            }
            Log.Debug("Debugging GetAllTableAsync Repository has been finised");
        
        }

        public async Task<Table> GetTableById(int Id)
        {
            try
            {
                Log.Debug("Debugging GetTableById  Service has been started");
                var result = await _context.Tables.FindAsync(Id);
                Log.Information($"Db Query has been get Table Id Service");
                Log.Debug($"Debugging GetMenuById Service Has been Finished Successfully With TableId ");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task UpdateTable(Table t)
        {
            try
            {
                Log.Debug("Debugging Create Table Service has been started");
                Table tt = new Table();
                t.TableNumber = t.TableNumber;
                await _context.AddAsync(t);
                await _context.SaveChangesAsync();
                Log.Information("db query has been add new Table Service");
                Log.Debug("Debugging Create Table Service has been finished");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}