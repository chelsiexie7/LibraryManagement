using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LibraryManagement.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
{
    if (!context.User.Identity.IsAuthenticated && 
        (context.Request.Path.StartsWithSegments("/Book/Create") ||
         context.Request.Path.StartsWithSegments("/Book/Edit") ||
         context.Request.Path.StartsWithSegments("/Book/Delete") ||
         context.Request.Path.StartsWithSegments("/Author/Delete") ||
         context.Request.Path.StartsWithSegments("/Author/Edit") ||
         context.Request.Path.StartsWithSegments("/Author/Create") ||
         context.Request.Path.StartsWithSegments("/Customer") ||
         context.Request.Path.StartsWithSegments("/LibraryBranch/Create") ||
         context.Request.Path.StartsWithSegments("/LibraryBranch/Edit") ||
         context.Request.Path.StartsWithSegments("/LibraryBranch/Delete")))
    {
        // set status code as 401
        
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.Redirect($"/Home/HandleError?statusCode={context.Response.StatusCode}");
        return;
    }

    await _next(context);
}

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
