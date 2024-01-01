using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using EmployeeVerificationSystem.Interface;
using EmployeeVerificationSystem.Models;

namespace EmployeeVerificationSystemApi.Controllers
{
    [EnableCors("EmployeeSystem")]
    [Route("api/Certifications")]
    [ApiController]
    public class CertificationsController : ControllerBase
    {
        readonly ICertification dal;
        readonly EmployeeContext _context;

        public CertificationsController(ICertification dal, EmployeeContext context)
        {
            this.dal = dal;
            _context = context;
        }

        [HttpGet]
        [Route("GetCertificationByEmployeeId/{eid}")]
        public List<Certification> GetCertificationByEmployeeId(int eid)
        {
            return dal.GetCertificationByEmployeeId(eid);
        }

        [HttpPost]
        [Route("AddCertification")]
        public string AddCertification(Certification cn)
        {
            return dal.AddCertification(cn);
        }

        [HttpDelete]
        [Route("DeleteCertifaction")]
        public bool DeleteCertifaction(int cid)
        {
            return dal.DeleteCertifaction(cid);
        }

        [HttpPut]
        [Route("UpdateCertification")]
        public async Task<int> UpdateCertification(Certification certification)
        {
            return await dal.UpdateCertification(certification);
        }
    }
}
