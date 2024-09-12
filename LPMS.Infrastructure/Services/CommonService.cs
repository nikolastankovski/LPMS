using Microsoft.AspNetCore.Http;

namespace LPMS.Infrastructure.Services;

public class CommonService : ICommonService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LPMSViewsDbContext _viewsContext;   

    public CommonService(IHttpContextAccessor httpContextAccessor, LPMSViewsDbContext viewsContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _viewsContext = viewsContext;
    }

    public LoggedInUser? GetLoggedInUser()
    {
        if (_httpContextAccessor.HttpContext == null) return null;

        var email = _httpContextAccessor.HttpContext.User?.Identity?.Name ?? string.Empty;

        var loggedInUser = _viewsContext.vwApplicationUsers
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
