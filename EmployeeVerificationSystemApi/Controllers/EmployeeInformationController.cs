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
    [Authorize]
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
            return _mapper.Map<List<EmployeeInfoModel>>(this.dal.GetEmplyeeList());
        }


        [HttpGet]
        [Route("GetEmployeeById")]
        public EmployeeInfo GetEmployeeById(int eid)
        {
            return this.dal.GetEmployeeById(eid);
        }

        [HttpPost]
        [Route("AddEmplyees")]
        public bool AddEmplyee(EmployeeInfo emp)
        {
            return this.dal.AddEmplyee(emp);
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
            return await this.dal.UpdateEmployee(emp);
        }
    }
}
