using System;
using System.Collections.Generic;
using System.Text;
using Core.Extensions.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
