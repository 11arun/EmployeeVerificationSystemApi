using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EmployeeVerificationSystem.Interface;
using EmployeeVerificationSystem.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EmployeeVerificationSystemApi.Models;
using System.Collections.Generic;

namespace EmployeeVerificationSystemApi.Controllers
{
    [EnableCors("EmployeeSystem")]
    [Route("api/EmployeeInformation")] 
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
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Route("GetEmplyeesList")]
        public List<EmployeeInfoModel> GetEmplyeeList()
        {
            var result = this.dal.GetEmplyeeList();
            return _mapper.Map<List<EmployeeInfoModel>>(result);
        }


        [HttpGet]
        [Authorize]
        [Route("GetEmployeeById")]
        public EmployeeInfoModel GetEmployeeById(int eid)
        {
            return _mapper.Map<EmployeeInfoModel>(this.dal.GetEmployeeById(eid));
        }

        [HttpPost]
       // [Authorize]
        [Route("AddEmplyees")]
        public bool AddEmplyee(EmployeeInfoModel emp)
        {
            var empinfo = _mapper.Map<EmployeeInfo>(emp);
            return this.dal.AddEmplyee(empinfo);
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteEmplyee")]
        public bool DeleteEmplyee(int eid)
        {
            return dal.DeleteEmplyee(eid);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateEmployees")]
        public async Task<int> UpdateEmployee(EmployeeInfo emp)
        {
            var empinfo = _mapper.Map<EmployeeInfo>(emp);
            return await this.dal.UpdateEmployee(empinfo);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public string ResetPassword(string email, string password)
        {
            return dal.ResetPassword(email, password) ? "Password Reset successfull" : "Email not found";
        }
    }
}
