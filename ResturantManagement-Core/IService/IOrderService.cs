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
        Task<List<OrderDto>> GetAllOrderAsync();
        Task<OrderDto> GetOrderById(int Id);
        Task CreateOrder(OrderDto dto);
       public Task UpdateOrder(OrderDto dto);
        Task DeleteOrder(int Id);
    }
}
