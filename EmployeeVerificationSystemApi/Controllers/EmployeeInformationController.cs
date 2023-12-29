using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EmployeeVerificationSystem.Interface;
using EmployeeVerificationSystem.Models;

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

        public EmployeeInformationController(IEmployeeInfo dal, EmployeeContext context)
        {
            this.dal = dal;
            this.context = context;
        }

       [HttpGet]
        [Route("GetEmplyeesList")]
        public List<EmployeeInfo> GetEmplyeeList()
        {
            return this.dal.GetEmplyeeList();
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
