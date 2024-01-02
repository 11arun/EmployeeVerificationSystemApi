using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using EmployeeVerificationSystem.Interface;
using EmployeeVerificationSystem.Models;

namespace EmployeeVerificationSystemApi.Controllers
{
    [EnableCors("EmployeeSystem")]
    [Route("api/WorkExperience")]

    [ApiController]
    public class WorkExperienceController : ControllerBase
    {
        readonly IWorkExp workExp;
        readonly EmployeeContext employeeContext;
        public WorkExperienceController(IWorkExp workExp, EmployeeContext context)
        {
            this.workExp = workExp;
            this.employeeContext = context;
        }

        [HttpGet]
        [Route("GetEmpWorkExp")]
        public List<WorkExperience> GetWorkExperiences(int empId)
        {
            try
            {
                return employeeContext.WorkExperiences.Where(x => x.EmpId == empId).ToList();
            }
            catch
            {
                throw;

            }


        }

        [HttpGet]
        [Route("GetEmpWorkExpList")]

        public List<WorkExperience> GetWorkExpList()
        {
            try { return this.workExp.GetWorkExpList(); }
            catch { throw; }
            
        }

        [HttpPost]
        [Route("AddWorkExp")]

        public bool AddWorkExp(WorkExperience workExperience)
        {
            try { return this.workExp.AddWorkExp(workExperience); }
            catch { throw;  }
            
        }

        [HttpDelete]
        [Route("DeleteWorkExp")]

        public bool DeleteWorkExp(int cId) {
            try { return this.workExp.DeleteWorkExp(cId); }
            catch { throw; }
        }

        [HttpPut]
        [Route("UpdateWorkExp")]

        public async Task<int> UpdateWorkExp(WorkExperience workExperience)
        {
            return await this.workExp.UpdateWorkExp(workExperience);
        }


    }
}
