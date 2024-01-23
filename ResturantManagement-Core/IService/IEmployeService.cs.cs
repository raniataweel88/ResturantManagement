using ResturantManagement_Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IService
{
    public interface IEmployeService
    {
        Task<List<EmployeDTO>> GetAllEmployeAsync();
        Task GetEmployeById(int Id);
        Task CreateEmploye(EmployeDTO dto);
        Task UpdateEmploye(EmployeDTO dto);
        Task DeleteEmploye(int Id);
    }
}
