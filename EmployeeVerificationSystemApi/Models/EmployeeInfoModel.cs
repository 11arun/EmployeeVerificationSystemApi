namespace EmployeeVerificationSystemApi.Models
{
    public class EmployeeInfoModel
    {
        public int EmpId { get; set; }

        public string? Name { get; set; }

        public string? MobNo { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool? IsActive { get; set; }

        public Guid? RoleId { get; set; }
    }
}
