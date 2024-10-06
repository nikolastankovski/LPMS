using Microsoft.AspNetCore.Http;

namespace LPMS.Infrastructure.Services;

public class CommonService : ICommonService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LPMSDbContext _context;   

    public CommonService(IHttpContextAccessor httpContextAccessor, LPMSDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public LoggedInUser? GetLoggedInUser()
    {
        if (_httpContextAccessor.HttpContext == null) return null;

        var email = _httpContextAccessor.HttpContext.User?.Identity?.Name ?? string.Empty;

        var loggedInUser = _context.vwApplicationUsers
                .Where(x => x.Email == email)
                .Select(x => new LoggedInUser()
                {
                    AccountId = x.AccountId,
                    SystemUserId = x.SystemUserId,
                    Name = x.Name,
                    Role = x.Role
                })
                .FirstOrDefault();


        return loggedInUser;
    }
}
