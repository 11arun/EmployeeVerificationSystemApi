using AutoMapper;
using EmployeeVerificationSystem.Models;
using EmployeeVerificationSystemApi.Models;

namespace EmployeeVerificationSystemApi.Mappper
{
    public class AutoMapper: Profile
    {
        public AutoMapper() {
            CreateMap<EmployeeInfoModel, EmployeeInfo>();
        }
    }
}
