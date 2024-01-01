using EmployeeVerificationSystem.Interface;
using EmployeeVerificationSystem.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;



namespace EmployeeVerificationSystemApi.Controllers
{
    [EnableCors("EmployeeSystem")]
    [Route("api/Educational")]
    [ApiController]
    public class EducationalController : ControllerBase
    {
        readonly IEducational dal;
        readonly EmployeeContext context;

        public EducationalController(IEducational dal, EmployeeContext context)
        {
            this.dal = dal;
            this.context = context;
        }
        [HttpGet]
        [Route("GetEducationalList")]
        public List<EducationalBackground> GetEducationalList()
        {
            return this.dal.GetEducationalList();
        }
        [HttpGet]
        [Route("GetEducationalById")]
        public EducationalBackground GetEducationalById(int EduId)
        {
            return this.dal.GetEducationalById(EduId);
        }

        [HttpPost]
        [Route("AddEducational")]
        public bool AddEducational(EducationalBackground educational)
        {
            return this.dal.AddEducational(educational);
        }
        [HttpDelete]
        [Route("DeleteEducational")]
        public bool DeleteEducational(int EduId)
        {
            return this.dal.DeleteEducational(EduId);
        }
        [HttpPut]
        [Route("UpdateEDucational")]
        public async Task<string> UpdateEDucational(EducationalBackground educational)
        {
            var result= await this.dal.UpdateEDucational(educational);
            return result ? "Educational details updated" : "Unable to update !";
        }
    }
}
