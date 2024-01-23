using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.Helper;
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
        public async Task CreateOrderItem(OrderItemDto dto )
        {
            Log.Debug("Debugging Create Order Item Service has been started");
            OrderItem o = new OrderItem();
            o.OrderId = dto.OrderId;
            o.MenuId= dto.MenuId;
            o.Quantity=dto.Quantity; 
            MenuDTO menu=new MenuDTO();
            OrderDto or=new OrderDto();
            await _context.AddAsync(o);
            await _context.SaveChangesAsync();
            var result = await _context.Orders.FindAsync(or.OrderId.Equals(dto.OrderId)); 
            if(result != null)
            {
            or.TotalPrice =menu.Price * o.Quantity;
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
                Log.Debug($"Debugging DeleteOrderItem Service has been finished");
            }
            Log.Error("OrderItem Not Found");
        }

        public async Task<List<OrderItemDto>> GetAllOrderItemAsync()
        {
            Log.Debug("Debugging GetAllOrderItemAsync Service has been started");
            var OrderItem = await _context.OrderItems.ToListAsync();
            var result = from o in OrderItem
                         select new OrderItemDto
                         {
                             OrderId=o.OrderId,
                             MenuId = o.MenuId,
                             OrderItemId = o.OrderItemId,
                             Quantity= o.Quantity,

                         };
            Log.Information("Db query has been Get All OrderItem Service");
            Log.Debug("Debugging GetAllOrderItemAsync Service has been finised");
            return (result.ToList());
        }

        public async Task GetOrderItemById(int Id)
        {
            Log.Debug("Debugging GetOrderItemById Service has been started");
            var result = await _context.OrderItems.AnyAsync(x => x.OrderItemId == Id);
            Log.Information($"Db Query has been get OrderItem Id Service");
            Log.Debug($"Debugging GetOrderItemById Service Has been Finised Successfully With OrderItemId");
        }

        public async Task UpdateOrderItem(OrderItemDto dto)
        {
            Log.Debug($"Debugging UpdateOrderItem Service has been started");
            var result = await _context.OrderItems.FindAsync(dto.OrderItemId);
            result.Quantity=dto.Quantity;
            result.MenuId=dto.MenuId;
            result.OrderId=dto.OrderId;
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Service");
            Log.Debug($"Debugging OrderItem Service has been Finshied"); ;
        }
        #endregion

    }
}


