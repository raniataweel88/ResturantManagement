using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResturantManagement_Core.DTO.Authentication;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ResturantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly RestrantDbContext _context;
        public AuthenticationController(RestrantDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Email))
                throw new Exception("Email Is Required");
            if (string.IsNullOrEmpty(dto.Password))
                throw new Exception("Password Is Required");
                var Customer = _context.Customers.SingleOrDefault(x =>
            x.Email.Equals(dto.Email) && x.Password.Equals(dto.Password));
                if (Customer != null)
                {
                    if (!Customer.IsLoggedIn)
                    {
                        Customer.IsLoggedIn = true;
                        //Generate Access Key
                        Customer.AccessKey = Guid.NewGuid().ToString();
                    Customer.AccesskeyExpireDate = DateTime.Now.AddHours(5);
                        _context.Update(Customer);
                        await _context.SaveChangesAsync();
                        return Ok(Customer.AccessKey);
                    }

                    Customer.IsLoggedIn = false;
                    _context.Update(Customer);
                    await _context.SaveChangesAsync();
                    return Ok("Your Session Has been Closed Please Login in Again");
                
            }
            else
            {
                var Employee = _context.Employes.SingleOrDefault(x => x.Email.Equals(dto.Email) && x.Password.Equals(dto.Password) );

                if (Employee != null)
                {
                    if (!Employee.IsLoggedIn)
                    {
                        Employee.IsLoggedIn = true;
                        //Generate Access Key
                        Employee.AccessKey = Guid.NewGuid().ToString();
                        Employee.AccesskeyExpireDate = DateTime.Now.AddHours(5);
                        _context.Update(Employee);
                        await _context.SaveChangesAsync();
                        return Ok(Employee.AccessKey);
                    }

                    Employee.IsLoggedIn = false;
                    _context.Update(Employee);
                    await _context.SaveChangesAsync();
                    return Ok("Your Session Has been Closed Please Login in Again");

                }
              
            }  return Unauthorized("Either Email or Password is Incorrect");
        } 

        [HttpPost]
        [Route("Register")]
        public async Task Register(RegisterDTO dto)
        {
            Customer c = new Customer();
            c.Email = dto.Email;
            c.Name = dto.Name;
            c.Password = dto.Password;
            c.Phone = dto.PhoneNumber;   
            c.IsLoggedIn = false;
            await _context.Customers.AddAsync(c);
            await _context.SaveChangesAsync();
        
        }
    }
}
    

