using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.IRepository
{
    public interface IOrderItemRepository
    {
         Task<List<OrderItem>> GetAllOrderItemAsync();
        Task<OrderItem> GetOrderItemById(int Id);
        Task CreateOrderItem(OrderItem o);
        Task UpdateOrderItem(OrderItem o);
        Task DeleteOrderItem(int Id);
    }
}
