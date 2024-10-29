using System.ComponentModel.DataAnnotations;

namespace LPMS.Domain.Models.RnRModels.AuthModels
{
    public class LoginRequest
    {
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
