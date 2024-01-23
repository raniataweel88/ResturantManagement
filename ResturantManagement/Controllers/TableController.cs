using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantManagement_Core.DTO;
using ResturantManagement_Infra.Service ;

namespace ResturantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly TableService  _Service ;
        public TableController(TableService  Service )
        {
            _Service  = Service ;
        }
        /// <summary>
        /// Add data of Table
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Post api/CreateTable
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", 
        ///        "price":"Enter your price of item"
        ///     }
        /// </remarks>
        /// <returns>Add Table</returns>
        /// <response code="201">Returns the new data of Table </response>
        /// <response code="400">If the error was occured</response>     
        [HttpPost]
        [Route("[action]")]
        public Task CreateTable(TableDto dto)
        {
            return (_Service .CreateTable(dto));
        }
        /// <summary>
        /// return list of Table
        /// </summary>
        /// <returns>all data Table</returns>
        /// <response code="201">Returns the all data of Table </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public Task<List<TableDto>> GetAllTableAsync()
        {

            return (_Service .GetAllTableAsync());
        }
        /// <summary>
        /// return the Table by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    GET api/GetTableById
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///     }
        /// </remarks>
        /// <returns>Returns Table</returns>
        /// <response code="201">Returns the  data of Table </response>
        /// <response code="400">If the error was occured</response>    
        [HttpGet]
        [Route("[action]")]
        public Task GetTableById(int Id)
        {
            return (_Service .GetTableById(Id));
        }
        /// <summary>
        /// Update the data of Table
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Put api/UpdateTable
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", 
        ///        "price":"Enter your price of item"
        ///     }
        /// </remarks>
        /// <returns>Update Table</returns>
        /// <response code="201">Update the old data of Table </response>
        /// <response code="400">If the error was occured</response>    
        [HttpPut]
        [Route("[action]")]
        public Task UpdateTable(TableDto dto)
        {
            return _Service .UpdateTable(dto);
        }
        /// <summary>
        /// DElET the data of Table
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    DElET api/DeleteTable
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///     }
        /// </remarks>
        /// <returns>DElET  Table</returns>
        /// <response code="201">DElET the  data of Table </response>
        /// <response code="400">If the error was occured</response>    

        [HttpDelete]
        [Route("[action]")]
        public Task DeleteTable(int Id)
        {
            return _Service .DeleteTable(Id);
        }
    }
}
