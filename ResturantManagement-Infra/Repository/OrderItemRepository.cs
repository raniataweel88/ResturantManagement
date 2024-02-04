using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.IRepository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Infra.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public OrderItemRepository(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        #region OrderItem
        public async Task CreateOrderItem(OrderItem o)
        {
            try {  
                Log.Debug("Debugging Create Order Item Repository has been started");
            OrderItem or = new OrderItem();
            or.OrderId = o.OrderId;
            or.MenuId = o.MenuId;
            or.Quantity = o.Quantity;
            await _context.AddAsync(or);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new item Repository"); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Db Query can not add new item");
            }
            Log.Debug("Debugging Create OrderItem Repository has been finished");
        }

        public async Task DeleteOrderItem(int Id)
        {
            Log.Debug($"Debugging DeleteOrderItem Repository has been started");
            try {
            var result = _context.OrderItems.FindAsync(Id);
            if (result != null)
            {
                Log.Information("OrderItem Is Exist");
                _context.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes the item of OrderItem successfully Repository");
            }
            }
            catch (Exception ex)
            {
                Log.Error("OrderItem Not Found");
                throw new Exception(ex.Message + "OrderItem Not Found");
            }
            Log.Debug($"Debugging DeleteOrderItem Repository has been finished");

        }

        public async Task<List<OrderItem>> GetAllOrderItemAsync()
        {
            Log.Debug("Debugging GetAllOrderItemAsync Repository has been started");
            try
            {
                var OrderItem = await _context.OrderItems.ToListAsync();
                var result = from o in OrderItem
                             select new OrderItem
                             {
                                 MenuId = o.MenuId,
                                 OrderItemId = o.OrderItemId,
                                 OrderId = o.OrderId,
                                 Quantity = o.Quantity,
                             };
                Log.Information("Db query has been Get All OrderItem Repository");
            return (result.ToList());}
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Db Query donot  have objects of model OrderItem");
            }
            Log.Debug("Debugging GetAllOrderItemAsync Repository has been finished");
            
        }

        public async Task<OrderItem> GetOrderItemById(int Id)
        {
            Log.Debug("Debugging GetOrderItemById Repository has been started");
            try
            {
                var result = await _context.OrderItems.FindAsync(Id);
                Log.Information($"Db Query has been get OrderItem Id Repository");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Db Query has not been get OrderItem Id ");
            }
            Log.Debug($"Debugging GetOrderItemById Repository Has been Finished Successfully With OrderItemId");
        }
        public async Task UpdateOrderItem(OrderItem o)
        {
            Log.Debug($"Debugging UpdateOrderItem  Repository has been started");
            try
            {
            var result = await _context.OrderItems.FindAsync(o.OrderItemId);
            result.MenuId=o.MenuId;
            result.OrderId=o.OrderId;
            result.Quantity=o.Quantity;
            _context.Update(result);
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Repository");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Db Query has not been updates OrderItem ");
            }
            Log.Debug($"Debugging OrderItem Repository has been Finished"); ;
        }
        #endregion
    }
}
