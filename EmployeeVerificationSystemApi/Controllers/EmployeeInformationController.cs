using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EmployeeVerificationSystem.Interface;
using EmployeeVerificationSystem.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EmployeeVerificationSystemApi.Models;

namespace EmployeeVerificationSystemApi.Controllers
{
    [EnableCors("EmployeeSystem")]
    [Route("api/EmployeeInformation")]
   // [Authorize]
    [ApiController]
    public class EmployeeInformationController : ControllerBase
    {
        // Using this we will call EmployeeInformation class's method
        readonly IEmployeeInfo dal;
        readonly EmployeeContext context;
        private readonly IMapper _mapper;
        public EmployeeInformationController(IMapper mapper, IEmployeeInfo dal, EmployeeContext context)
        {
            this.dal = dal;
            this.context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("GetEmplyeesList")]
        public List<EmployeeInfoModel> GetEmplyeeList()
        {
            var result = this.dal.GetEmplyeeList();
            return _mapper.Map<List<EmployeeInfoModel>>(result);
        }


        [HttpGet]
        [Route("GetEmployeeById")]
        public EmployeeInfo GetEmployeeById(int eid)
        {
            return dal.GetEmployeeById(eid);
        }

        [HttpPost]
        [Route("AddEmplyees")]
        public string AddEmplyee(EmployeeInfo emp)
        {
            return dal.AddEmplyee(emp) ? "Employee Added successfully" : "Email/Mobile is already exists !";
        }

        [HttpDelete]
        [Route("DeleteEmplyee")]
        public bool DeleteEmplyee(int eid)
        {
            return dal.DeleteEmplyee(eid);
        }

        [HttpPut]
        [Route("UpdateEmployees")]
        public async Task<int> UpdateEmployee(EmployeeInfo emp)
        {
            return await dal.UpdateEmployee(emp);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public string ResetPassword(string email, string password)
        {
            return dal.ResetPassword(email, password) ? "Password Reset successfull" : "Email not found";
        }
    }
}
