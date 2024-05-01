using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPMS.Domain.Interfaces.ServiceInterfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(Guid userId);
        Task<string> GenerateTokenAsync(string email);
    }
}
