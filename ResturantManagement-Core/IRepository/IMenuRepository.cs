using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IRepository
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetAllMenuAsync();
        Task GetMenuById(int Id);
        Task CreateMenu(Menu m);
        Task UpdateMenu(Menu m);
        Task DeleteMenu(int Id);


    }
}
