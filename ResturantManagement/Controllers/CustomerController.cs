using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.IService;
using ResturantManagement_Infra.Service;
using ResturantManagement_Infra.Service;

namespace ResturantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerService _Service;
        public CustomerController(CustomerService Service )
        {
            _Service=Service ;
        }
        /// <summary>
        /// Add data of Customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Post api/CreateCustomer
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", 
        ///     }
        /// </remarks>
        /// <returns>Add Customer</returns>
        /// <response code="201">Returns the new data of Customer </response>
        /// <response code="400">If the error was occured</response>     
        [HttpPost]
       [Route("[action]")]
        public Task CreateCustomer([FromBody]CustomerDTO dto)
        {
            return (_Service.CreateCustomer(dto));
        }
        /// <summary>
        /// return list of Customer
        /// </summary>
        /// <returns>all data Customer</returns>
        /// <response code="201">Returns the all data of Customer </response>
        /// <response code="400">If the error was occured</response>     
        [HttpGet]
        [Route("[action]")]
        public Task<List<CustomerDTO>> GetAllCustomerAsync()
        {

            return (_Service.GetAllCustomerAsync());
        }
        /// <summary>
        /// return the Customer by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    GET api/GetCustomerById
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///       
        ///     }
        /// </remarks>
        /// <returns>Returns Customer</returns>
        /// <response code="201">Returns the  data of Customer </response>
        /// <response code="400">If the error was occured</response>    
        [HttpGet]
        [Route("[action]")]
        public Task GetCustomerById([FromRoute] int Id)
        {
            return (_Service .GetCustomerById(Id));
        }
        /// <summary>
        /// Update the data of Customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Put api/UpdateCustomer
        ///     {        
        ///            "Id": "Enter Id  Here",   
        ///        "Name": "Enter  Name of item  Here", 
        ///     }
        /// </remarks>
        /// <returns>Update Customer</returns>
        /// <response code="201">Update the old data of Customer </response>
        /// <response code="400">If the error was occured</response>    
        [HttpPut]
        [Route("[action]")]
        public Task UpdateCustomer([FromBody] CustomerDTO dto)
        {
            return _Service .UpdateCustomer(dto);
        }
        /// <summary>
        /// DElET the data of Customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    DElET api/DeleteCustomer
        ///            "Id": "Enter Id  Here",   
        ///     }
        /// </remarks>
        /// <returns>DElET  Customer</returns>
        /// <response code="201">DElET the  data of Customer </response>
        /// <response code="400">If the error was occured</response>    

        [HttpDelete]
        [Route("[action]")]
        public Task DeleteCustomer([FromRoute] int Id)
        {
            return _Service .DeleteCustomer(Id);
        }
    }
}
