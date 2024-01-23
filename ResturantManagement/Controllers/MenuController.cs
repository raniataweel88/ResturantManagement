using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantManagement_Core.DTO;
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
        private readonly MenuService  _Service ;
        private readonly OrderItemService _Service1;
        public MenuController(MenuService  Service , OrderItemService Service1)
        {
            _Service  = Service ;
            _Service1 = Service1;
        }
        #region Mune
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
        public  Task CreateMenu(MenuDTO dto)
        {
            return (_Service.CreateMenu(dto));
        }
        /// <summary>
        /// return list of mune
        /// </summary>
        /// <returns>all data Menu</returns>
        /// <response code="201">Returns the all data of menu </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public  Task<List<MenuDTO>> GetAllMenuAsync()
        {
                return (_Service .GetAllMenuAsync());
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
        /// <response code="201">Returns the  data of menu </response>
        /// <response code="400">If the error was occured</response>    
        [HttpGet]
        [Route("[action]")]
        public  Task GetMenuById(int Id)
    {
       return (_Service .GetMenuById(Id));
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
        public  Task UpdateMenu(MenuDTO dto)
    {
      return  _Service.UpdateMenu(dto);
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
        [Route("[action]")]
        public  Task DeleteMune(int Id)
    {
            return _Service.DeleteMenu(Id);   
    }
        #endregion
        #region OrderItem
        /// <summary>
        /// Add data of OderItem
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Post api/CreateOrderItem
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", 
        ///     }
        /// </remarks>
        /// <returns>Add OrderItem</returns>
        /// <response code="201">Returns the new data of OrderItem </response>
        /// <response code="400">If the error was occured</response>     
        [HttpPost]
        [Route("[action]")]
        public Task CreateOrder(OrderItemDto dto)
        {
            return (_Service1.CreateOrderItem(dto));
        }
        /// <summary>
        /// return list of OrderItem
        /// </summary>
        /// <returns>all data OrderItem</returns>
        /// <response code="201">Returns the all data of OrderItem </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public Task<List<OrderItemDto>> GetAllOrderItemAsync()
        {
            return (_Service1.GetAllOrderItemAsync());
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
        [Route("[action]")]
        public Task GetOrderItemById(int Id)
        {
            return (_Service1.GetOrderItemById(Id));
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
        public Task UpdateOrderItem(OrderItemDto dto)
        {
            return _Service1.UpdateOrderItem(dto);
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
        [Route("[action]")]
        public Task DeleteOrderItem(int Id)
        {
            return _Service1.DeleteOrderItem(Id);
        }
    
    #endregion
}
}
