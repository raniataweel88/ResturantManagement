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
    public class MenuRepository : IMenuRepository
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public MenuRepository(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        #region Menu
        public async Task CreateMenu(Menu m)
        {
            Log.Debug("Debugging Create Menu Repository has been started");
           Menu menu = new Menu();
            menu.Name = m.Name;
            menu.Price = m.Price;
            await _context.AddAsync(menu);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new item Repository");
            Log.Debug("Debugging Create Menu Repository has been finished");
        }

        public async Task DeleteMenu(int Id)
        {
            Log.Debug($"Debugging Delete Menu Repository has been started");
            var result = _context.Menus.FindAsync(Id);
            if (result != null)
            {
                Log.Information("Menu Is Exist");
                _context.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes the item of Menu Repository successfully");
                Log.Debug($"Debugging Delete Menu Repository has been finished");
            }
            Log.Error("Menu Not Found");
        }

        public async Task<List<Menu>> GetAllMenuAsync()
        {
            Log.Debug("Debugging GetAllMenu Repository has been started");
            var Menu = await _context.Menus.ToListAsync();
            var result = from M in Menu
                         select new Menu
                         {
                             MenuId=M.MenuId,
                             Name=M.Name,
                             Price=M.Price,
                         };
            Log.Information("Db query has been Get All Menu Repository");
            Log.Debug("Debugging GetAllMenu Repository has been finished");
            return (result.ToList());
        }  
      
        public async Task GetMenuById(int Id)
        {
                Log.Debug("Debugging GetMenuById Repository has been started");
                  var result = await _context.Menus.AnyAsync(x => x.MenuId==Id);
                Log.Information($"Db Query has been get Menu Id Repository");
                 Log.Debug($"Debugging GetMenuById Repository Has been Finised Successfully With MenuId");
        }

        public async Task UpdateMenu(Menu m)
        {
            Log.Debug($"Debugging UpdateMenu Repository has been started");
            var result = await _context.Menus.FindAsync(m.MenuId);
            result.Name = m.Name;
            result.Price = m.Price;
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Menu Repository");
            Log.Debug($"Debugging UpdateMenu Repository has been Finished"); ;
        }
        #endregion
    }
}
