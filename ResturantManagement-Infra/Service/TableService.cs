using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task CreateTable(TableDTO dto)
        {
            try { 
             Log.Debug("Debugging Create Table Service has been started");
            Table t = new Table();
            t.TableNumber = dto.TableNumber;
            await _context.Tables.AddAsync(t);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new Table Service");
            Log.Debug("Debugging Create Table Service has been finished");
            }
           catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteTable(int Id)
        {
            Log.Debug($"Debugging DeleteTable Service has been started");
            try
            {
                var result = await _context.Tables.FindAsync(Id);
                if (result != null)
                {
                    Log.Information("Table Is Exist");
                    _context.Tables.Remove(result);
                    await _context.SaveChangesAsync();
                    Log.Information("Db Query deletes the Table Service successfully");
                    Log.Debug($"Debugging DeleteTable Service has been finished");
                }
                else
                {     
                    Log.Error("Table Not Found");
                    throw new Exception("the table not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
            public async Task<List<TableDTO>> GetAllTableAsync()
        {  Log.Debug("Debugging GetAllTableAsync Service has been started");
            try {
            var Table = await _context.Tables.ToListAsync();
            var result = from t in Table
                         select new TableDTO
                         {
                             TableId = t.TableId,
                             TableNumber = t.TableNumber,
                         };
            Log.Information("Db query has been Get All Table Service");
            Log.Debug("Debugging GetAllTableAsync Service has been finished");
            return (result.ToList());
             }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
             }
}
        public async Task<TableDTO> GetTableById(int Id)
        {
            try
            {
             Log.Debug("Debugging GetTableById  Service has been started");
                
            var result = await _context.Tables.FindAsync(Id);
                if (result != null)
                { 
                    TableDTO table = new TableDTO()
                    {
                        TableId = result.TableId,
                        TableNumber = result.TableNumber,
                }; 
                    Log.Information($"Db Query has been get Table Id Service");
                   return table;
                }
                else
                {
                    throw new Exception("the table not found");
                }

                Log.Debug($"Debugging GetMenuById Service Has been Finished Successfully With TableId ");

            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
        public async Task UpdateTable(TableDTO dto)
        {
            try {    
            Log.Debug($"Debugging UpdateTable Service has been started");
            var result = await _context.Tables.FindAsync(dto.TableId);
                if(result != null)
                {
                 result.TableId = dto.TableId;
                  result.TableNumber=dto.TableNumber;
                  _context.Tables.Update(result);
                await _context.SaveChangesAsync();
                Log.Information($"Db has been updates Service");
                }
                else
                {
                    throw new Exception("the table not found");
                }
                Log.Debug($"Debugging UpdateTable Service has been Finished"); 
              
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
       
        }
        #endregion
    }
}