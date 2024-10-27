using LPMS.Domain.Models.Entities;
using LPMS.Domain.Models.Entities.IdentityEntities;
using LPMS.Domain.Models.RnRModels.NewFolder;

namespace LPMS.Domain.Models.RnRModels.UserManagementModels
{
    public class CreateModifyUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        //public List<DepartmentxDivisionRequest> DepartmentsWithDivisions { get; set; } = new List<DepartmentxDivisionRequest>();


        public SystemUser MapToSystemUser()
        {
            return new SystemUser()
            {
                Email = Email,
                UserName = Email,
                PhoneNumber = PhoneNumber,
                PhoneNumberConfirmed = true
            };
        }

        public Account MapToAccount(Guid sysUserId)
        {
            return new Account()
            {
                Name = Name,
                SystemUserId = sysUserId
            };
        }

        /*public List<AccountxDepartmentxDivision> MapToAccountxDepartmentxDivision(Guid accountId)
        {
            var accxDeptxDiv = new List<AccountxDepartmentxDivision>();

            foreach (var department in DepartmentsWithDivisions)
            {
                foreach (var division in department.Divisions)
                {
                    accxDeptxDiv.Add(new AccountxDepartmentxDivision()
                    {
                        AccountId = accountId,
                        DepartmentId = department.Department,
                        DivisionId = division
                    });
                }
            }

            return accxDeptxDiv;
        }*/
    }
}
