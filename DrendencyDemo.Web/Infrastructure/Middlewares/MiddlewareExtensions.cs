using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DrendencyDemo.Web.Infrastructure.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AddExceptionHandler(this IApplicationBuilder app)
        {
            app.UseWhen(ctx => ctx.Request.Path.HasValue && ctx.Request.Path.StartsWithSegments(new PathString("/api")),
                b => b.UseMiddleware<ApiExceptionHandlerMiddleware>());
            return app;
        }
    }
}
