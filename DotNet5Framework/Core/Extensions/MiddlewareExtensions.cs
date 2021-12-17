using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Core.Extensions.Middlewares;

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
