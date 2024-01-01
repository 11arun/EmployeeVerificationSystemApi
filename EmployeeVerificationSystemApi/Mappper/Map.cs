using AutoMapper;
using EmployeeVerificationSystem.Models;
using EmployeeVerificationSystemApi.Models;

namespace EmployeeVerificationSystemApi.Mappper
{
    public class Map: Profile
    {
        public Map() {
            CreateMap<EmployeeInfo,EmployeeInfoModel>();
            CreateMap<EmployeeInfoModel,EmployeeInfo>();
        }
    }
}
