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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ResturantManagement_Infra.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly RestrantDbContext _context;
        public OrderRepository(IConfiguration configuration, RestrantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        #region Order
        public async Task CreateOrder(Order o)
        {
            Log.Debug("Debugging Create Order Repository has been started");
            Order or = new Order();
            try {    
                or.TotalPrice = 0;
            await _context.AddAsync(or);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new order Repository"); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Db Query has not been add new order");
            }
            Log.Debug("Debugging Create Order Repository has been finished");
        }

        public async Task DeleteOrder(int Id)
        {
            Log.Debug($"Debugging DeleteOrder Repository has been started");
            try { var result = _context.OrderItems.FindAsync(Id);
                if (result != null)
                {
                    Log.Information("Order Is exist");
                    _context.Remove(result);
                    await _context.SaveChangesAsync();
                    Log.Information("Db Query deletes Repository the item of Order successfully");
                }
            }
            catch (Exception ex) {
                Log.Error("Order Not Found");
                throw new Exception(ex.Message + "Order Not Found");
            }
               
            Log.Debug($"Debugging DeleteOrder Repository has been finished");
        }

        public async Task<List<Order>> GetAllOrderAsync()
        {
            Log.Debug("Debugging GetAllOrderAsync Repository has been started");
            try { var Order = await _context.Orders.ToListAsync();
            var result = from o in Order
                         select new Order
                         {
                             TotalPrice = o.TotalPrice,
                             OrderId = o.OrderId,
                         };
            Log.Information("Db query has been Get All Order Repository");  
                return (result.ToList());}
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Db Query donot  have objects of model Order");
            }
            Log.Debug("Debugging GetAllOrderAsync has been finished Repository");
      
        }
        public async Task<Order> GetOrderById(int Id)
        {
            Log.Debug("Debugging GetOrderById Repository has been started ");
            try
            {
             var result = await _context.Orders.FindAsync(Id);
            Log.Information($"Db Query has been get Order Id Repository");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Db Query does not  have object of model Order");
            }
            Log.Debug($"Debugging GetOrderItemById Repository Has been Finished Successfully With OrderId");
        }
        public async Task UpdateOrder(Order o)
        {
            Log.Debug($"Debugging UpdateOrder Repository has been started");
            try
            {
            var result = await _context.Orders.FindAsync(o.OrderId);
            var OrderItems = await _context.OrderItems.FindAsync(o.OrderId);
            var Menu = await _context.Menus.FindAsync(o.OrderId);
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
            Log.Information($"Db has been updates Order Repository");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Db does not has been updates Order Repository");
            }
            Log.Debug($"Debugging OrderItem Repository has been Finished"); ;
        }
        #endregion
    }
}
