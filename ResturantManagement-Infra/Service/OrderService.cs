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
            o.OrderItemId = dto.OrderItemId;
            o.CustomerId = dto.CustomerId;
            o.EmployeId = dto.EmployeId;
            o.TableId = dto.TableId;
            await _context.AddAsync(o);
            await _context.SaveChangesAsync();
            Log.Information("db query has been add new order");
            Log.Debug("Debugging Create Order Service has been finished");
        }

        public async Task DeleteOrder(int Id)
        {
            Log.Debug($"Debugging DeleteOrder Service has been started");
            var result =await _context.Orders.FindAsync(Id);
            if (result != null)
            {
                Log.Information("Order Is exist");
                _context.Orders.Remove(result);
                await _context.SaveChangesAsync();
                Log.Information("Db Query deletes Service the item of Order successfully");
                Log.Debug($"Debugging DeleteOrder Service has been finished");
            }
            else
            {
                throw new Exception("the Order not found");
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
                             CustomerId = o.CustomerId,
                              OrderItemId = o.OrderItemId,
                             EmployeId= o.EmployeId,
                             TableId = o.TableId,


                         };
            Log.Information("Db query has been Get All Order Service");
            Log.Debug("Debugging GetAllOrderAsync has been finished Service");
            return (result.ToList());
        }
        public async Task<OrderDto> GetOrderById(int Id)
        {
            Log.Debug("Debugging GetOrderById Service has been started ");
            try
            {
                Log.Information($"Db Query has been get Order Id Service");
                var result = await _context.Orders.FindAsync(Id);
                if (result != null)
                {
                    OrderDto OrderDt = new OrderDto()
                    {
                        CustomerId = result.CustomerId,
                        OrderId = result.OrderId,
                        OrderItemId = result.OrderItemId,
                        TotalPrice = result.TotalPrice,
                        TableId= result.TableId,
                        EmployeId = result.EmployeId,
                    };
                    Log.Information($"Db Query has been get order Id Service");
                    return OrderDt;
                }
                else
                {
                    throw new Exception("the Order not found");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("can not get order  by id");
            }
      
        Log.Debug($"Debugging GetOrderItemById Service Has been Finished Successfully With OrderId");
          }public async Task UpdateOrder(OrderDto dto)
        {
            Log.Debug($"Debugging UpdateOrder Service has been started");
            var result = await _context.Orders.FindAsync(dto.OrderId);
            var OrderItems = await _context.OrderItems.FindAsync(dto.OrderId);
            var Menu = await _context.Menus.FindAsync(dto.OrderId);
            if (OrderItems != null&& result!=null&&Menu!=null)
            {
                result.TotalPrice = OrderItems.Quantity * Menu.Price;
                _context.Orders.Update(result);
            }
            else
            {
                result.TotalPrice = 0;
                _context.Orders.Update(result);
            }
           
            await _context.SaveChangesAsync();
            Log.Information($"Db has been updates Order Service");
            Log.Debug($"Debugging OrderItem Service has been Finished"); ;
        }
        #endregion
    }
}

