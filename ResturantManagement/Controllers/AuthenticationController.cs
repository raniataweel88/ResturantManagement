using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResturantManagement_Core.DTO.Authentication;
using ResturantManagement_Core.EntityFramework.Context;
using ResturantManagement_Core.EntityFramework.Models;
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
        [Route("Register")]
        public async Task Register()
        {

        }
        [HttpPost]
        [Route("Login")]
        public async Task Login(LoginDTO dto)
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
                    //1- Generate Random Value and Store it in data base
                    //user.AccessKey = Guid.NewGuid().ToString(); 
                    //user.AccesskeyExpireDate= DateTime.Now.AddMinutes(5);
                    _context.Update(Customer);
                    await _context.SaveChangesAsync();
                    //2-JWT 
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes("LongSecrectStringForModulekodestartppopopopsdfjnshbvhueFGDKJSFBYJDSAGVYKDSGKFUYDGASYGFskc vhHJVCBYHVSKDGHASVBCL");
                    var tokenDescriptior = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("CustomerId",Customer.CustomerId.ToString()),
                        new Claim("Name",Customer.Name)
                        }),
                        Expires = DateTime.Now.AddHours(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey)
                        , SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptior);//encoding
                    string finalToken = tokenHandler.WriteToken(token);
                    // return Ok(/*user.AccessKey*/finalToken);
                }


            }
        }
    }
    }

