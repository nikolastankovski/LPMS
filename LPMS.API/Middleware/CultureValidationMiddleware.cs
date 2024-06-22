using Microsoft.AspNetCore.Mvc;

namespace LPMS.API.Middleware
{
    internal class CultureValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var culture = context.Request.RouteValues["culture"];

            if(!Cultures.All.Contains(culture))
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = $"Culture not available. Available cultures: {string.Join(", ", Cultures.All)}.",
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(problemDetails);
                return;
            }

            await _next(context);
        }
    }
}
