using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPMS.Application.Models.RnRModels.Auth
{
    public class AuthTokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime? Expires { get; set; }
    }
}
