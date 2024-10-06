using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPMS.Domain.Models.RnRModels.UserManagementModels
{
    public class ApplicationUserResponse
    {
        public Guid AccountId { get; set; }
        public Guid SystemUserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
