using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.IService;
using ResturantManagement_Infra.Service ;
using Serilog;

namespace ResturantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly RestrantDbContext _context;

        private readonly IOrderService _Service;
        public OrderController(IOrderService Service, RestrantDbContext context)
        {
            _Service = Service;
            _context = context;
        }
        /// <summary>
        /// Add data of Order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Post api/CreateOrder
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", } 
        /// </remarks>
        /// <returns>Add Order</returns>
        /// <response code="201">Returns the new data of Order </response>
        /// <response code="400">If the error was occured</response>     
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto dto, [FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return BadRequest("Please Provide Your Access Key");

            }
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now) || await _context.Customers.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {
                var result = _Service.CreateOrder(dto);
                return Ok(result);
            }
            else
            {
                return Unauthorized("Please Provide Your Access Key");

            }
        }
        /// <summary>
        /// return list of Order
        /// </summary>
        /// <returns>all data Order</returns>
        /// <response code="201">Returns the all data of Order </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public async Task<List<OrderDto>> GetAllOrderAsync([FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return null;

            }
            if (_context.Employes.Any(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {
                return await _Service.GetAllOrderAsync();
            }
            else
            return null;
        }
        /// <summary>
        /// return the Order by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    GET api/GetOrderById
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///     }
        /// </remarks>
        /// <returns>Returns Order</returns>
        /// <response code="201">Returns the  data of Order </response>
        /// <response code="400">If the error was occured</response>    
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOrderById( int Id, [FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return BadRequest("Please Provide Your Access Key");

            }
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now) || await _context.Customers.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {
                return Ok(await _context.Orders.FindAsync(Id));
            }
            else
            {
                return Unauthorized("Please Provide Your Access Key");

            }

        }
        /// <summary>
        /// Update the data of Order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Put api/UpdateOrder
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", 
        ///     }
        /// </remarks>
        /// <returns>Update Order</returns>
        /// <response code="201">Update the old data of Order </response>
        /// <response code="400">If the error was occured</response>    
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto dto, [FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return BadRequest("Please Provide Your Access Key");
            }
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now) ||await _context.Customers.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {
                var re =_Service.UpdateOrder(dto);
                return Ok(re);
            
            }
            else
            {
                return Unauthorized("Please Provide Your Access Key");
            }
        }
        /// <summary>
        /// DElET the data of Order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    DElET api/DeleteOrder
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///     }
        /// </remarks>
        /// <returns>DElET  Order</returns>
        /// <response code="201">DElET the  data of Order </response>
        /// <response code="400">If the error was occured</response>    

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int Id, [FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return BadRequest("Please Provide Your Access Key");
            }

            if ( await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now) ||await _context.Customers.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {
                var re = _Service.DeleteOrder(Id);
                return Ok(re);
            }
            else
            {
                return Unauthorized("Please Provide Your Access Key");
            }
         }
    } }