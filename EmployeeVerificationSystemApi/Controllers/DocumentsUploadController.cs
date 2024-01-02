using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EmployeeVerificationSystem.Interface;
namespace EmployeeVerificationSystemApi.Controllers
{
    [EnableCors("EmployeeSystem")]
    [Route("api/[controller]")]
    [ApiController]

    
    public class DocumentsUploadController : ControllerBase
    {
        readonly IDocumentsUpload dal;

        public DocumentsUploadController(IDocumentsUpload dal)
        {
            this.dal = dal;
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task UploadFileToS3(IFormFile file)
        {
            await dal.UploadFileToS3(file);
        }

    }
}
