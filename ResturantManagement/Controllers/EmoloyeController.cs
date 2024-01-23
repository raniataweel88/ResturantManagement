using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.IService ;
using ResturantManagement_Infra.Service ;

namespace ResturantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmoloyeController : ControllerBase
    {
        private readonly EmployeService  _Service ;
        public EmoloyeController(EmployeService  Service )
        {
            _Service  = Service ;
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
        ///     }
        /// </remarks>
        /// <returns>Add Employe</returns>
        /// <response code="201">Returns the new data of Employe </response>
        /// <response code="400">If the error was occured</response>     
        [HttpPost]
        [Route("[action]")]
        public Task CreateEmploye(EmployeDTO dto)
        {
            return (_Service .CreateEmploye(dto));
        }
        /// <summary>
        /// return list of Employe
        /// </summary>
        /// <returns>all data Employe</returns>
        /// <response code="201">Returns the all data of Employe </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public Task<List<EmployeDTO>> GetAllEmployeAsync()
        {

            return (_Service .GetAllEmployeAsync());
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
        /// <response code="201">Returns the  data of Employe </response>
        /// <response code="400">If the error was occured</response>    
        [HttpGet]
        [Route("[action]")]
        public Task GetEmployeById(int Id)
        {
            return (_Service .GetEmployeById(Id));
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
        public Task UpdateEmploye(EmployeDTO e)
        {
            return _Service .UpdateEmploye(e);
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
        [Route("[action]")]
        public Task DeleteEmploye(int Id)
        {
            return _Service .DeleteEmploye(Id);
        }
    }
}
