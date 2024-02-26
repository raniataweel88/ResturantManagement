using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.IService;
using ResturantManagement_Infra.Service ;
using Serilog;

namespace ResturantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
              private readonly RestrantDbContext _context;
        private readonly ITableService  _Service ;
        public TableController(ITableService  Service, RestrantDbContext context)
        {
            _Service  = Service ;
            _context = context;
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
        ///        "TableNumber": "Enter  Number Here", 
        ///   "posistion":"Administrator or staff"
        ///     }
        /// </remarks>
        /// <returns>Add Table</returns>
        /// <response code="201">Returns the new data of Table </response>
        /// <response code="400">If the error was occured</response>     
        [HttpPost]
        [Route("[action]")]
        public async Task CreateTable([FromBody] TableDTO dto, [FromHeader] string email, [FromHeader] string pass)
        {
            if (await(_context.Employes.AnyAsync(x => x.Email == email && x.Password == pass && x.Position=="Administrator")))
                { 
                var r =  (_Service.CreateTable(dto));
               
            }
            else
            throw new Exception("dont have acces");           
        }
        /// <summary>
        /// return list of Table
        /// </summary>
        /// <returns>all data Table</returns>
        /// <response code="201">Returns the all data of Table </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public Task<List<TableDTO>> GetAllTableAsync([FromHeader] string email, [FromHeader] string pass)
        {
            if (_context.Employes.Any(x => x.Email == email && x.Password == pass)|| _context.Customers.Any(x => x.Email == email && x.Password == pass))
            {
                return (_Service.GetAllTableAsync());
            }
            else
            {
                Log.Error("you do not have validates access");
                throw new Exception("you do not have validates access");
            }
        
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
        [Route("[action]/{Id}")]
        public  async  Task<IActionResult> GetTableById([FromRoute] int Id)
        {

            return Ok(await _Service.GetTableById(Id));
          
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
        /// <response code="200">Update the old data of Table </response>
      
        [HttpPut]
        [Route("[action]")]
        public  Task UpdateTable([FromBody] TableDTO dto)
        {
           
               return _Service.UpdateTable(dto);
         
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
        /// <response code="200">DElET the  data of Table </response>
        /// <response code="400">If the error was occured</response>    

        [HttpDelete]
       [ Route("[action]/{Id}")]
        public async Task<IActionResult> DeleteTable(int Id, [FromHeader] string email, [FromHeader] string pass)
        {
            if (await _context.Employes.AnyAsync(x => x.Email == email && x.Password == pass && x.Position.Equals("Administrator")))
            {
            await _Service.DeleteTable(Id);
                return Ok("done Delete Table");
            }
            else
            {
                Log.Error("you do not have validates access");
            //    throw new Exception("you do not have validates access");
                return StatusCode(400, "you should Administrator ");
            }
        }
    }
}
