using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Infra.Service ;

namespace ResturantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService  _Service ;
        public OrderController(OrderService  Service )
        {
            _Service  = Service ;
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
        ///        "Name": "Enter  Name of item  Here", 
        ///     }
        /// </remarks>
        /// <returns>Add Order</returns>
        /// <response code="201">Returns the new data of Order </response>
        /// <response code="400">If the error was occured</response>     
        [HttpPost]
        [Route("[action]")]
        public Task CreateOrder(OrderDto dto)
        {
            return (_Service.CreateOrder(dto));
        }
        /// <summary>
        /// return list of Order
        /// </summary>
        /// <returns>all data Order</returns>
        /// <response code="201">Returns the all data of Order </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public Task<List<OrderDto>> GetAllOrderAsync()
        {

            return (_Service .GetAllOrderAsync());
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
        ///       
        ///     }
        /// </remarks>
        /// <returns>Returns Order</returns>
        /// <response code="201">Returns the  data of Order </response>
        /// <response code="400">If the error was occured</response>    
        [HttpGet]
        [Route("[action]")]
        public Task GetOrderById(int Id)
        {
            return (_Service.GetOrderById(Id));
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
        public Task UpdateOrder(OrderDto dto)
        {
            return _Service .UpdateOrder(dto);
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
        public Task DeleteOrder(int Id)
        {
            return _Service .DeleteOrder(Id);
        }
    }
}
