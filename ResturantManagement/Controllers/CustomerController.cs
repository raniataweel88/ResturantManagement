using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.IService;
using ResturantManagement_Infra.Service;
using Serilog;

namespace ResturantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly RestrantDbContext _context;
        private readonly ICustomerService _Service;
        public CustomerController(ICustomerService Service,RestrantDbContext context )
        {
            _Service=Service ;
            _context = context;
        }
        [HttpGet]
        [Route("[action]")]
        /// <summary>
        /// return list of Customer
        /// </summary>
        /// <returns>all data Customer</returns>
        /// <response code="201">Returns the all data of Customer </response>
        /// <response code="500">If the error was occured</response>  
        public Task<List<CustomerDTO>> GetAllCustomerAsync([FromHeader] string accessKey)
        {
            //access just for employee
            if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");
                return null;

            }
            if (_context.Employes.Any(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now))
            {
                return _Service.GetAllCustomerAsync();
            }
            else
            {
                Log.Information("Access Key not found");
                return null;
            }
      
        
          
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
        ///         "accessKey": "Enter accessKey  Here to check the validates",   
        ///     }
        /// </remarks>
        /// <returns>Returns Customer</returns>
        /// <response code="201">Returns the  data of Customer </response>
        /// <response code="400">If the not found</response>    
        /// <response code="500">If the error was occured</response>    
        [HttpGet]
        [Route("[action]/{Id}")]
        public async  Task<IActionResult> GetCustomerById(int Id, [FromHeader] string accessKey) {
            try
            {//just employee becouse if user know the id can access the data of anther customer
                if (string.IsNullOrEmpty(accessKey))
                {
                    Log.Information("Access Key not found");
                      return StatusCode(400,new {Response= "Access Key not found" });
                }
                if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
                x.AccesskeyExpireDate > DateTime.Now))
                {
                    var result= await _Service.GetCustomerByIdAsync(Id);
                    return Ok(result);
                }
                Log.Information("you cannot access");
                return StatusCode(500, new { Response = "you cannot access" });
                } 
            catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
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
        ///       "Name": "Enter  Name of item  Here", 
        ///       "Email": "Enter Id  Here",   
        ///       "password": "Enter  password of Here", 
        ///       "phone": "Enter phone number  Here",   
        ///     }
        /// </remarks>
        /// <returns>Add Customer</returns>

        [HttpPost]
         [Route("[action]")]
        public Task CreateCustomer([FromBody]CustomerDTO dto)
        {
            try
            { 
                return (_Service.CreateCustomer(dto));
            } 
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }        
        }
        /// <summary>
        /// Update the data of Customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Put api/UpdateCustomer
        ///     {        
        ///       "Id": "Enter Id  Here",   
        ///       "Name": "Enter  Name of item  Here", 
        ///       "Email": "Enter Id  Here",   
        ///       "password": "Enter  password of Here", 
        ///       "phone": "Enter phone number  Here",  
        ///     }
        /// </remarks>
        /// <returns>Update Customer</returns>
        /// <response code="201">Update the old data of Customer </response>
        /// <response code="400">If the error was occured</response>    
        [HttpPut]
        [Route("[action]")]
        public async Task UpdateCustomer([FromBody] CustomerDTO dto, [FromHeader] string accessKey)
        {
                try
                {
                    if (string.IsNullOrEmpty(accessKey))
                    {
                        Log.Information("Access Key not found");
                        throw new Exception("Please Provide Your Access Key");
                    }
                    if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
                    x.AccesskeyExpireDate > DateTime.Now) || (await _context.Customers.AnyAsync(x => x.AccessKey == accessKey &&
                    x.AccesskeyExpireDate > DateTime.Now)))
                    {

                      _Service.UpdateCustomer(dto);
                }
                  
                }
                catch (Exception ex)
                {
                throw new Exception(ex.Message);
                }
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
        /// <response code="200">DElET the  data of Customer </response>
        /// <response code="400">not found accessKey</response>    
        /// <response code="500">If the error was occured</response>   
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteCustomer(int Id, [FromHeader] string accessKey)
        {
            try{  if (string.IsNullOrEmpty(accessKey))
            {
                Log.Information("Access Key not found");  
                return StatusCode(400,new { Response="Please Provide Your Access Key" });
            }
            if (await _context.Employes.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now) ||(await _context.Customers.AnyAsync(x => x.AccessKey == accessKey &&
            x.AccesskeyExpireDate > DateTime.Now)))
            {
            await _Service.DeleteCustomer(Id);
          return Ok("Customer deleted successfully");
                }
            else
            {
                return Unauthorized("Please Provide Your Access Key");

            }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Response = ex.Message});
            }
        }
    }
}
