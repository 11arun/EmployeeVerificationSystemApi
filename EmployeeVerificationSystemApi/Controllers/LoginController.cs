﻿using EmployeeVerificationSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EmployeeVerificationSystemApi.Controllers
{
    [Route("api/EmployeeLogin")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly IEmployeeLogin dal;
        private IConfiguration configuration;
        [HttpPost]
        [Route("EmployeLogin")]
        public IActionResult Login(string Email,string Password)
        {
            IActionResult result = Unauthorized();
            var response = dal.AuthenticateEmployee(Email, Password);
            if (response)
            {
                var Token = GenerateToken();
                result = Ok(new { token = Token });
            }
            return result;
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
