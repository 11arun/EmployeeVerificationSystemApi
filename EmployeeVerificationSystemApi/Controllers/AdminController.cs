using EmployeeVerificationSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace EmployeeVerificationSystemApi.Controllers
{
    [EnableCors("EmployeeSystem")]
    [Route("api/AdminController")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        readonly EmployeeContext db;
       
        public AdminController(EmployeeContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("GetApproveStatus/{eid}/{cid}")]
        public bool GetApproveStatus(int eid, int cid)
        {
         var res = db.WorkExperiences.Where(x=>x.EmpId == eid && x.Cid == cid).FirstOrDefault();
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
