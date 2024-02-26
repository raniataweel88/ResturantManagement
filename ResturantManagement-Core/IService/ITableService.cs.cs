using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IService
{
    public interface ITableService
    {
        Task<List<TableDTO>> GetAllTableAsync();
        Task<TableDTO> GetTableById(int Id);
        Task CreateTable(TableDTO dto);
        Task UpdateTable(TableDTO dto);
        Task DeleteTable(int Id);
    }
}
