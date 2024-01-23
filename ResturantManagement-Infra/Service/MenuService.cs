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
    public class MenuService : IMenuService
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public MenuService(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        #region Menu
        public async Task CreateMenu(MenuDTO dto)
        {
            Log.Debug("Debugging Create menu Service has been started");
            Menu menu = new Menu();
            menu.Name = dto.Name;
            menu.Price = dto.Price;
            await _context.AddAsync(menu);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new item Service");
            Log.Debug("Debugging Create menu Service has been finished");
        }

        public async Task DeleteMenu(int Id)
        {
            Log.Debug($"Debugging DeleteMenu Service has been started");
            var result = _context.Menus.FindAsync(Id);
            if (result != null)
            {
                Log.Information("Menu Is exist");
                _context.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes the item of Menu Service successfully");
                Log.Debug($"Debugging DeleteMenu Service  has been finished");
            }
            Log.Error("Question Type Not Found");
        }

        public async Task<List<MenuDTO>> GetAllMenuAsync()
        {
            Log.Debug("Debugging GetAllMenuAsync Service has been started");
            var Menu = await _context.Menus.ToListAsync();
            var result = from M in Menu
                         select new MenuDTO
                         {
                             MenuId = M.MenuId,
                             Name = M.Name,
                             Price = M.Price,
                         };
            Log.Information("Db query has been Get All Menu Service");
            Log.Debug("Debugging GetAllMenuAsync Service has been finised");
            return (result.ToList());
        }

        public async Task GetMenuById(int Id)
        {
            Log.Debug("Debugging GetMenuById Service has been started");
            var result = await _context.Menus.AnyAsync(x => x.MenuId == Id);
            Log.Information($"Db Query has been get Menu Id Service");
            Log.Debug($"Debugging GetMenuById Service Has been Finished Successfully With MenuId");
        }

        public async Task UpdateMenu(MenuDTO dto)
        {
            Log.Debug($"Debugging UpdateMenu Service has been started");
            var result = await _context.Menus.FindAsync(dto.MenuId);
            result.Name = dto.Name;
            result.Price = dto.Price;
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Service");
            Log.Debug($"Debugging UpdateMenu Service has been Finished"); ;
        }
        #endregion
    }
}
