using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
            try
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
            catch (Exception ex)
            {
                throw new Exception("can not Create Menu");
            }
        }

            public async Task DeleteMenu(int Id)
        {
            Log.Debug($"Debugging DeleteMenu Service has been started");
            try
            {
                var result =await _context.Menus.FindAsync(Id);
                if (result != null)
                {
                    Log.Information("Menu Is exist");
                    _context.Menus.Remove(result);
                    await _context.SaveChangesAsync();
                    Log.Information("Db Query deletes the item of Menu Service successfully");
                    Log.Debug($"Debugging DeleteMenu Service  has been finished");
                }
                else
                {
                    Log.Error("Menu Not Found");
                    throw new Exception(" Menu not found");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("can not Delete Menu");
            }
        }

    public async Task<List<MenuDTO>> GetAllMenuAsync()
        {
            Log.Debug("Debugging GetAllMenuAsync Service has been started");
            try
            {
                var Menu = await _context.Menus.ToListAsync();
                if (Menu != null)
                {
                    var result = from M in Menu
                                 select new MenuDTO
                                 {
                                     MenuId = M.MenuId,
                                     Name = M.Name,
                                     Price = M.Price,
                                 };
                    return (result.ToList());
                    Log.Information("Db query has been Get All Menu Service");
                    Log.Debug("Debugging GetAllMenuAsync Service has been finished");
                }
                else
                {
                    throw new Exception("can notfound menu");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" cannot Get All MenuAsync");
            }     
        }
 public async Task<MenuDTO> GetMenuById(int Id)
        { 
            Log.Debug("Debugging GetMenuById Service has been started");
            try { 
            var result = await _context.Menus.FindAsync(Id);
            if (result != null)
            {
                MenuDTO MenuDTO = new MenuDTO()
                {
                   Name=result.Name, 
                    Price=result.Price,
                    MenuId=result.MenuId
                };
                Log.Information($"Db Query has been get Menu Id Service");
                return MenuDTO;
            }
            else
                    throw new Exception("can not found menu");

            }
            catch (Exception ex)
            {
                throw new Exception("can not get Menu by id");
             }
            Log.Debug($"Debugging GetMenuById Service Has been Finished Successfully With MenuId");
          

}
        public async Task UpdateMenu(MenuDTO dto) 
        { Log.Debug($"Debugging UpdateMenu Service has been started");
            try
            {
           var result = await _context.Menus.FindAsync(dto.MenuId);
                if (result != null)
                {
                 result.Name = dto.Name;
                 result.Price = dto.Price;
                _context.Menus.Update(result);
                 await _context.SaveChangesAsync();
                        Log.Information($"Db has been updates Service");
                }
                else
                {
                    throw new Exception("can notfound menu");
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");   
            }
            Log.Debug($"Debugging UpdateMenu Service has been Finished"); ;
        }
        #endregion
    }
}
