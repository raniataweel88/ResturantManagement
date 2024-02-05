using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.IRepository;
using ResturantManagement_Core.IService;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Infra.Service
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public OrderItemService(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

      #region OrderItmem
        public async Task CreateOrderItem( OrderItemDto dto)
        {
                Log.Debug("Debugging Create Order Item Service has been started");
                OrderItem o = new OrderItem() {
                    OrderId = dto.OrderId,
                  MenuId = dto.MenuId,
                  Quantity = dto.Quantity,
                 };
                   
                await _context.AddAsync(o);
                await _context.SaveChangesAsync();
            Menu menu = new Menu();
                Order or = new Order();
                var result = await _context.Orders.FindAsync(or.OrderId.Equals(dto.OrderId));
                if (result != null)
                {
                    or.TotalPrice+=menu.Price * o.Quantity;
                    result.TotalPrice = or.TotalPrice;
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                Log.Information("db query has been add new item Service");
                Log.Debug("Debugging Create OrderItem Service has been finished");
        }

        public async Task DeleteOrderItem(int Id)
        {      
            Log.Debug($"Debugging DeleteOrderItem Service has been started");
 
                var result = _context.OrderItems.FindAsync(Id);
                if (result != null)
                {
                    Log.Information("OrderItem Is Exist");
                    _context.Remove(result);
                    await _context.SaveChangesAsync();
                    Log.Information("Db Query deletes the item of OrderItem Service successfully");
                }
               else { Log.Error("OrderItem Not Found");
                }
           
            Log.Debug($"Debugging DeleteOrderItem Service has been finished");
            }
        public async Task<List<OrderItemDto>> GetAllOrderItemAsync()
        {
           
                Log.Debug("Debugging GetAllOrderItemAsync Service has been started");
                var OrderItem = await _context.OrderItems.ToListAsync();
                var result = from o in OrderItem
                             select new OrderItemDto
                             {
                                OrderItemId = o.OrderId,
                                MenuId = o.MenuId,
                                OrderId = o.OrderId,
                                Quantity = o.Quantity,
                             };
                Log.Information("Db query has been Get All OrderItem Service");
                return (result.ToList());
            Log.Debug("Debugging GetAllOrderItemAsync Service has been finished");
           
        }

        public async Task<OrderItemDto> GetOrderItemById(int Id)
        {
            try { 
                Log.Debug("Debugging GetOrderItemById Service has been started");
                var result = await _context.OrderItems.FindAsync(Id);
                Log.Information($"Db Query has been get OrderItem Id Service");
                Log.Debug($"Debugging GetOrderItemById Service Has been Finished Successfully With OrderItemId");
            if (result != null)
            {
                OrderItemDto OrderItemDt = new OrderItemDto()
                {
                    Quantity= result.Quantity,
                    OrderId = result.OrderId,
                    OrderItemId = result.OrderItemId,
                    MenuId=result.MenuId,
                };
                Log.Information($"Db Query has been get orderitem Id Service");
                return OrderItemDt;
            }
            else
                return null;
        } 
          catch(Exception ex)
            {
            throw new Exception("can not get order item by id");
    }
}

        public async Task UpdateOrderItem(OrderItemDto dto)
        {
              Log.Debug($"Debugging UpdateOrderItem Service has been started");
                var result = await _context.OrderItems.FindAsync(dto.OrderItemId);
                result.Quantity = dto.Quantity;
                result.MenuId = dto.MenuId;
                result.OrderId = dto.OrderId;
                _context.Update(result);
                await _context.SaveChangesAsync();
                Log.Information($"Db has been updates Service");
     
            Log.Debug($"Debugging OrderItem Service has been Finished"); ;
        }
        #endregion

    }
}


