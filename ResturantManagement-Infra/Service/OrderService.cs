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
    public class OrderService : IOrderService
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public OrderService(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        #region Order
        public async Task CreateOrder(OrderDto dto)
        {
            Log.Debug("Debugging Create Order Service has been started");
            Order o = new Order();
            o.TotalPrice = 0;
            await _context.AddAsync(o);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new order");
            Log.Debug("Debugging Create Order Service has been finished");
        }

        public async Task DeleteOrder(int Id)
        {
            Log.Debug($"Debugging DeleteOrder Service has been started");
            var result = _context.OrderItems.FindAsync(Id);
            if (result != null)
            {
                Log.Information("Order Is exist");
                _context.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes Service the item of Order successfully");
                Log.Debug($"Debugging DeleteOrder Service has been finished");
            }
            Log.Error("Order Not Found");
        }

        public async Task<List<OrderDto>> GetAllOrderAsync()
        {
            Log.Debug("Debugging GetAllOrderAsync Service has been started");
            var Order = await _context.Orders.ToListAsync();
            var result = from o in Order
                         select new OrderDto
                         {
                             TotalPrice = o.TotalPrice,
                             OrderId = o.OrderId,
                         };
            Log.Information("Db query has been Get All Order Service");
            Log.Debug("Debugging GetAllOrderAsync has been finished Service");
            return (result.ToList());
        }
        public async Task GetOrderById(int Id)
        {
            Log.Debug("Debugging GetOrderById Service has been started ");
            var result = await _context.Orders.AnyAsync(x => x.OrderId == Id);
            Log.Information($"Db Query has been get Order Id Service");
            Log.Debug($"Debugging GetOrderItemById Service Has been Finished Successfully With OrderId");
        }
        public async Task UpdateOrder(OrderDto dto)
        {
            Log.Debug($"Debugging UpdateOrder Service has been started");
            var result = await _context.Orders.FindAsync(dto.OrderId);
            var OrderItems = await _context.OrderItems.FindAsync(dto.OrderId);
            var Menu = await _context.Menus.FindAsync(dto.OrderId);
            if (OrderItems != null)
            {
                result.TotalPrice = OrderItems.Quantity * Menu.Price;
                _context.Update(result);
            }
            else
            {
                result.TotalPrice = 0;
                _context.Update(result);
            }
           
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Order Service");
            Log.Debug($"Debugging OrderItem Service has been Finished"); ;
        }
        #endregion
    }
}

