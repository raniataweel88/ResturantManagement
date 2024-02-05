using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class EmoloyeController : ControllerBase
    {  private readonly RestrantDbContext _context;
        private readonly IEmployeService  _Service ;
        public EmoloyeController(IEmployeService  Service, RestrantDbContext context)
        {
            _Service  = Service ;
            _context = context;
        }
        /// <summary>
        /// return list of Employe
        /// </summary>
        /// <returns>all data Employe</returns>
        /// <response code="201">Returns the all data of Employe </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public async Task<List<EmployeDTO>> GetAllEmployeeAsync([FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return null;
            }
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {
                return await _Service.GetAllEmployeAsync();
            }
            return null;

        }
        /// <summary>
        /// return the Employe by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    GET api/GetEmployeById
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///       
        ///     }
        /// </remarks>
        /// <returns>Returns Employe</returns>
        /// <response code="200">Returns the  data of Employe </response>
        /// <response code="400">If the accesskey not found</response>  
        /// <response code="500">If the error was occured</response> 
        [HttpGet]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> GetEmployeById(int Id, [FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return StatusCode(400, new { Response = "Access Key not found"});

            }
            if (_context.Employes.Any(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {
                var get = _Service.GetEmployeById(Id);
                return Ok(get);
            }
            else
            {
                return StatusCode(500, new { Response = "do not have access" });
            }


        }
        /// <summary>
        /// Add data of Employe
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Post api/CreateEmploye
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", 
        ///        "position":Administrator or Staff
        ///     }
        /// </remarks>
        /// <returns>Add Employe</returns>
        /// <response code="200">Returns the new data of Employe </response>
        [HttpPost]
        [Route("[action]")]
        public Task CreateEmploye(EmployeDTO dto)
        {
            try
            {
             return (_Service.CreateEmploye(dto)); 
            }
              catch(Exception  ex)
            {
                throw new Exception(ex.Message);
            }
        }
     
        /// <summary>
        /// Update the data of Employe
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Put api/UpdateEmploye
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here",     
        ///     }
        /// </remarks>
        /// <returns>Update Employe</returns>
        /// <response code="201">Update the old data of Employe </response>
        /// <response code="400">If the error was occured</response>    
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateEmploye([FromBody]EmployeDTO e, [FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return BadRequest("Please Provide Your Access Key");

            }
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {

             var UpdateEmploye =  _Service.UpdateEmploye(e);
                return Ok(UpdateEmploye);
            }
            else
            {
                return Unauthorized("Please Provide Your Access Key");
            }
           
        }
        /// <summary>
        /// DElET the data of Employe
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    DElET api/UpdateEmploye
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///     }
        /// </remarks>
        /// <returns>DElET  Employe</returns>
        /// <response code="201">DElET the  data of Employe </response>
        /// <response code="400">If the error was occured</response>    

        [HttpDelete]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> DeleteEmploye( int Id, [FromHeader] string accessKey)
        {
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return BadRequest("Please Provide Your Access Key");

            }
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {
                await _Service.DeleteEmploye(Id);
                return Ok("Employee deleted successfully");
            }
            else
            {
                return Unauthorized("Please Provide Your Access Key");
            
            
            }

        }
       
    }
}
