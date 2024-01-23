using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IService
{
    public interface IOrderItemService
    {
        Task<List<OrderItemDto>> GetAllOrderItemAsync();
        Task GetOrderItemById(int Id);
        Task CreateOrderItem(OrderItemDto dto);
        Task UpdateOrderItem(OrderItemDto dto);
        Task DeleteOrderItem(int Id);
    }
}
