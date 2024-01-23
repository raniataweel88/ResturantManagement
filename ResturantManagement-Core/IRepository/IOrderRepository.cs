using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IRepository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrderAsync();
        Task GetOrderById(int Id);
        Task CreateOrder(Order o);
        Task UpdateOrder(Order o);
        Task DeleteOrder(int Id);
    }
}
