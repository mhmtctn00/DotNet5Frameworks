using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions.Middlewares
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "internal_server_error";

            if (e.Message == "authorization_denied")
            {
                httpContext.Response.StatusCode = 401;
                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = 401,
                    Message = e.Message
                }.ToString());
            }

            if (e.GetType() == typeof(ValidationException))
            {
                httpContext.Response.StatusCode = 200;
                message = e.Message;
            }

            return httpContext.Response.WriteAsync(new ErrorResult(resultCode: 15963, message: message).ToString());
        }
    }
}
