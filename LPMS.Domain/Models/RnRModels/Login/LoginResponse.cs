using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPMS.Application.Models.RnRModels.Login
{
    public record LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime? Expires { get; set; }
        public string RefreshToken { get; set; } = string.Empty;

    }
}
