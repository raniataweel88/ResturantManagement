using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.Helper
{
    public static class MappingHelper
    {
        public static List<OrderItemDTO> OrderItemDTOMapper(List<OrderItem> OrderItem)
        {
            List<OrderItemDTO> OrderItemDTO = new List<OrderItemDTO>();
            foreach (OrderItem orderItem in OrderItem)
            {
                OrderItemDTO dTO = new OrderItemDTO();
                dTO.MenuId = orderItem.MenuId;
              dTO.OrderId = orderItem.OrderId;
                dTO.Quantity= orderItem.Quantity;
                dTO.OrderItemId = orderItem.OrderItemId;
                OrderItemDTO.Add(dTO);
            }
            return OrderItemDTO;
        }
    }
}
