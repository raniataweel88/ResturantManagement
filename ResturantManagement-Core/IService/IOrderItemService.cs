using Microsoft.AspNetCore.Mvc;
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
        Task<List<OrderItemDTO>> GetAllOrderItemAsync();
        Task<OrderItemDTO> GetOrderItemById(int Id);
        Task CreateOrderItem(OrderItemDTO dto);
        Task UpdateOrderItem(OrderItemDTO dto);
        Task DeleteOrderItem(int Id);
    }
}
