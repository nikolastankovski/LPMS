using System.ComponentModel.DataAnnotations;

namespace LPMS.Domain.Models.RnRModels.UserManagementModels
{
    public class AuthenticationTokenRequest
    {
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
