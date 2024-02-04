using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IRepository
{
    public interface IEmployeRepository
    {
        Task<List<Employe>> GetAllEmployeAsync();
        Task<Employe> GetEmployeById(int Id);
        Task CreateEmploye(Employe e);
        Task UpdateEmploye(Employe e);
        Task DeleteEmploye(int Id);
    }
}
