using AutoMapper;
using EmployeeVerificationSystem.Interface;
using EmployeeVerificationSystem.Models;
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
        readonly EmployeeContext _context;
        IMapper _mapper;
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger,IMapper mapper, IEmployeeLogin dal, IConfiguration _configuration, EmployeeContext context)
        {
            this.dal = dal;
            this._context = context;
            _mapper = mapper;
            configuration= _configuration;
            _logger = logger;
        }
        [HttpPost]
        [Route("EmployeLogin")]
        public IActionResult Login(string Email,string Password)
        {
            _logger.LogInformation("Login Api call");
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

            var token = new JwtSecurityToken(issuer: configuration["Jwt:Issuer"],
              audience:configuration["Jwt:ValidAudience"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
