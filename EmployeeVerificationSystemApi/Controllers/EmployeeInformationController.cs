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
    //[Authorize]
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
        [Route("GetEmplyeesList")]
        public List<EmployeeInfoModel> GetEmplyeeList()
        {
            var result = this.dal.GetEmplyeeList();
            return _mapper.Map<List<EmployeeInfoModel>>(result);
        }


        [HttpGet]
        [Route("GetEmployeeById")]
        public EmployeeInfoModel GetEmployeeById(int eid)
        {
            return _mapper.Map<EmployeeInfoModel>(this.dal.GetEmployeeById(eid));
        }

        [HttpPost]
        [Route("AddEmplyees")]
        public bool AddEmplyee(EmployeeInfoModel emp)
        {
            var empinfo = _mapper.Map<EmployeeInfo>(emp);
            return this.dal.AddEmplyee(empinfo);
        }

        [HttpDelete]
        [Route("DeleteEmplyee")]
        public bool DeleteEmplyee(int eid)
        {
            return this.dal.DeleteEmplyee(eid);
        }

        [HttpPut]
        [Route("UpdateEmployees")]
        public async Task<int> UpdateEmployee(EmployeeInfo emp)
        {
            var empinfo = _mapper.Map<EmployeeInfo>(emp);
            return await this.dal.UpdateEmployee(empinfo);
        }
    }
}
