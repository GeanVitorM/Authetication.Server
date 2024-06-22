using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authetication.Server.Api.Middlewares
{
    public class JwtRoleExtractorMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtRoleExtractorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var claimsPrincipal = context.User;
            var roleClaim = claimsPrincipal.FindFirst(ClaimTypes.Role);
            var userIdClaim = claimsPrincipal.FindFirst("UserId");

            if (roleClaim != null)
            {
                var role = roleClaim.Value;
                context.Request.Headers["X-User-Role"] = role;
            }

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                context.Request.Headers["X-User-Id"] = userId;
            }

            await _next(context);
        }
    }

    public static class JwtRoleExtractorMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtRoleExtractor(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtRoleExtractorMiddleware>();
        }
    }
}
