using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.IService ;
using ResturantManagement_Infra.Service ;
using Serilog;

namespace ResturantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService  _Service ;
        private readonly IOrderItemService _Service1; 
        private readonly RestrantDbContext _context;
        public MenuController(IMenuService  Service , IOrderItemService Service1, RestrantDbContext context)
        {
            _Service  = Service ;
            _Service1 = Service1;
            _context = context;
            
       
        }
        #region Mune
        /// <summary>
        /// return list of mune
        /// </summary>
        /// <returns>all data Menu</returns>
        /// <response code="201">Returns the all data of menu </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public Task<List<MenuDTO>> GetAllMenuAsync()
        {
            return (_Service.GetAllMenuAsync());
        }
        /// <summary>
        /// return the menu by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    GET api/GetMenuById
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///       
        ///     }
        /// </remarks>
        /// <returns>Returns Menu</returns>
        /// <response code="200">Returns the  data of menu </response>
        /// <response code="400">If the error was occured</response>    
        [HttpGet]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> GetMenuById(int Id)
        {
            return Ok(await _Service.GetMenuById(Id));
        }
        /// <summary>
        /// Add data of Menu
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Post api/CreateMune
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", 
        ///        "price":"Enter your price of item"
        ///     }
        /// </remarks>
        /// <returns>Add Menu</returns>
        /// <response code="201">Returns the new data of menu </response>
        /// <response code="400">If the error was occured</response>     
        [HttpPost]
        [Route("[action]")]
        public  Task CreateMenu([FromBody] MenuDTO dto, [FromHeader] string email, [FromHeader] string pass)
        {
            if (_context.Employes.Any(x => x.Email == email && x.Password == pass && x.Position == "Administrator"))
            {
                return  _Service.CreateMenu(dto);
            }
            else 
            {   
                throw new Exception("you do not have validates access"); 
            }
         }   
     
        /// <summary>
        /// Update the data of Mune
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Put api/UpdateMune
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", 
        ///        "price":"Enter your price of item"
        ///       
        ///     }
        /// </remarks>
        /// <returns>Update Menu</returns>
        /// <response code="201">Update the old data of menu </response>
        /// <response code="400">If the error was occured</response>    
        [HttpPut]
        [Route("[action]")]
        public  Task UpdateMenu([FromBody] MenuDTO dto, [FromHeader] string email, [FromHeader] string pass)
         {
            if (_context.Employes.Any(x => x.Email == email && x.Password == pass && x.Position == "Administrator"))
            {
                return _Service.UpdateMenu(dto);
            }
            else
            {
                Log.Error("you do not have validates access");
                throw new Exception("you do  not have validates access");
            }
            }
        /// <summary>
        /// DElET the data of Mune
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    DElET api/UpdateMune
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///     }
        /// </remarks>
        /// <returns>DElET  Menu</returns>
        /// <response code="201">DElET the  data of menu </response>
        /// <response code="400">If the error was occured</response>    

        [HttpDelete]
        [Route("[action]/{Id}")]
        public async Task DeleteMune( int Id, [FromHeader] string email, [FromHeader] string pass)
        {
            if ( await _context.Employes.AnyAsync(x => x.Email == email && x.Password == pass && x.Position == "Administrator"))
            {
               await _Service.DeleteMenu(Id);
           }
            else
            {
                Log.Error("you do not have validates access");
                throw new Exception("you do not have validates access");
            }
        }
        #endregion
        #region OrderItem
        /// <summary>
        /// return list of OrderItem
        /// </summary>
        /// <returns>all data OrderItem</returns>
        /// <response code="201">Returns the all data of OrderItem </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public async Task<List<OrderItemDTO>> GetAllOrderItemAsync([FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                  throw new Exception("Please Provide Your Access Key");

            }
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now)){
            return await _Service1.GetAllOrderItemAsync();
            }
            else
            {
                throw new Exception("you con not access Access Key");
            }
           
        }
        /// <summary>
        /// return the OrderItem by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    GET api/GetOrderItemById
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///       
        ///     }
        /// </remarks>
        /// <returns>Returns OrderItem</returns>
        /// <response code="201">Returns the  data of OrderItem </response>
        /// <response code="400">If the error was occured</response>    
        [HttpGet]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> GetOrderItemById( int Id, [FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return BadRequest("Please Provide Your Access Key");

            }
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {

                return Ok(await _Service1.GetOrderItemById(Id));
            }
            else

                return Unauthorized("you can not access Access Key");
        }
        /// <summary>
        /// Add data of OderItem
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Post api/CreateOrderItem
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///       "Name": "Enter  Name of item  Here", 
        ///      "Description":Enter the Description  of item, 
        ///       "Price":Enter your price
        ///     }
        /// </remarks>
        /// <returns>Add OrderItem</returns>
        /// <response code="200">Returns the new data of OrderItem </response>
        /// <response code="400">If the error was occured</response>     
        /// 
        [HttpPost]
        [Route("[action]")]
        public async Task CreateOrderItem([FromBody]OrderItemDTO dto, [FromHeader] string accessKey)
        {
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
                        x.AccesskeyExpireDate > DateTime.Now))
            {
              await _Service1.CreateOrderItem(dto);
            }
            else
            {
            throw new Exception("cannot add create order");

            }
          
        }
       
        /// <summary>
        /// Update the data of OrderItem
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Put api/UpdateOrderItem
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", 
        ///     }
        /// </remarks>
        /// <returns>Update OrderItem</returns>
        /// <response code="201">Update the old data of OrderItem</response>
        /// <response code="400">If the error was occured</response>    
        [HttpPut]
        [Route("[action]")]
        public async Task UpdateOrderItem([FromBody]OrderItemDTO dto, [FromHeader] string accessKey)
        {
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
                         x.AccesskeyExpireDate > DateTime.Now))
            {

                await _Service1.UpdateOrderItem(dto);
            }
            throw new Exception("cannot add create order");
        }
        /// <summary>
        /// DElET the data of OrderItem
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    DElET api/DeleteOrderItem
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///     }
        /// </remarks>
        /// <returns>DElET  OrderItem</returns>
        /// <response code="201">DElET the  data of OrderItem </response>
        /// <response code="400">If the error was occured</response>    

        [HttpDelete]
        [Route("[action]/{Id}")]
        public async Task DeleteOrderItem(int Id, [FromHeader] string accessKey)
        {
                if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
                             x.AccesskeyExpireDate > DateTime.Now))
                {
                await _Service1.DeleteOrderItem(Id);
            }
                throw new Exception("cannot add create order");
            }

            #endregion
        }
}
