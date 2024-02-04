using Microsoft.AspNetCore.Mvc;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IService
{
    public interface IMenuService
    {
        public Task<List<MenuDTO>> GetAllMenuAsync();
        public Task<MenuDTO> GetMenuById(int Id);
        public Task CreateMenu(MenuDTO dto);
        public Task UpdateMenu(MenuDTO dto);
        public Task DeleteMenu(int Id);

    }
}
