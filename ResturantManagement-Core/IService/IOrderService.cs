using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IService
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrderAsync();
        Task<OrderDTO> GetOrderById(int Id);
        Task CreateOrder(OrderDTO dto);
       public Task UpdateOrder(OrderDTO dto);
        Task DeleteOrder(int Id);
    }
}
