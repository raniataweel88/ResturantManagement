using ResturantManagement_Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IService
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> GetAllMenuAsync();
        Task GetMenuById(int Id);
        Task CreateMenu(MenuDTO dto);
        Task UpdateMenu(MenuDTO dto);
        Task DeleteMenu(int Id);

    }
}
