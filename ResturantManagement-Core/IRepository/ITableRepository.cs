using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IRepository
{
    public interface ITableRepository
    {
        Task<List<Table>> GetAllTableAsync();
        Task GetTableById(int Id);
        Task CreateTable(Table t);
        Task UpdateTable(Table t);
        Task DeleteTable(int Id);
    }
}
