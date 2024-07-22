using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync();
    }
}
