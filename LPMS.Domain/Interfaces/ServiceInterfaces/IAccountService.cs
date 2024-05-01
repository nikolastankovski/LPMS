using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPMS.Domain.Interfaces.ServiceInterfaces
{
    public interface IAccountService
    {
        Task<string> LoginAsync(string email, string password);
    }
}
