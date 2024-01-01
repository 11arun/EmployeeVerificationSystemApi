using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EmployeeVerificationSystem.Models;
namespace EmployeeVerificationSystemApi.Controllers
{
    [EnableCors("EmployeeSystem")]
    [Route("api/EducationApprovalController")]
    [ApiController]
    public class EducationApprovalController : ControllerBase
    {
        readonly EmployeeContext db;

        public EducationApprovalController(EmployeeContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("GetApproveStatus/{eid}/{eduid}")]
        public bool GetApproveStatus(int eid, int eduid)
        {
            var res = db.EducationalBackgrounds.Where(x=>x.EmpId == eid && x.EduId == eduid).FirstOrDefault();
            if(res != null)
            {
                res.ApproveStatus = "Verified";
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
