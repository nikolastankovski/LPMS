using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace LPMS.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        public new string[]? Roles { get; set; }
        //public string[]? Departments { get; set; }
        //public string[]? Divisions { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            //var test = context.HttpContext.RequestServices.GetService<Iauth>
            if(Roles != null && Roles.Any())
            {
                List<string> userRoles = context.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();

                if (!userRoles.Intersect(Roles).Any())
                {
                    context.Result = new ContentResult() { ContentType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3", Content = "403 Forbidden", StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }
            }

            /*if (Departments != null && Departments.Any())
            {
                List<string> userDeparments = context.HttpContext.User.Claims.Where(x => x.Type == CustomClaims.Department).Select(x => x.Value).ToList();

                if (!userDeparments.Intersect(Departments).Any())
                {
                    context.Result = new ContentResult() { ContentType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3", Content = "403 Forbidden", StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }
            }*/

            /*if (Divisions != null && Divisions.Any())
            {
                List<string> userDivisions = context.HttpContext.User.Claims.Where(x => x.Type == CustomClaims.Division).Select(x => x.Value).ToList();
                if (!userDivisions.Intersect(Divisions).Any())
                {
                    context.Result = new ContentResult() { ContentType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3", Content = "403 Forbidden", StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }
            }*/
        }
    }
}
