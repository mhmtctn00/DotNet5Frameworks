using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Aspects.Autofac.Logging
{
    public class ExceptionLogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerService;
        private IHttpContextAccessor _httpContextAccessor;

        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }

            _loggerService = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.ToString();
            _loggerService.Error(JsonConvert.SerializeObject(logDetailWithException));
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                if (invocation.Arguments[i] != null)
                    logParameters.Add(new LogParameter
                    {
                        Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                        Value = invocation.Arguments[i],
                        Type = invocation.Arguments[i].GetType().Name
                    });
                else
                    logParameters.Add(new LogParameter
                    {
                        Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                        Value = "<NULL>",
                        Type = invocation.GetConcreteMethod().GetParameters()[i].ParameterType.ToString()
                    });
            }

            var logDetailWithException = new LogDetailWithException
            {
                ClassName = invocation.TargetType.FullName,
                MethodName = invocation.Method.Name,
                User = (_httpContextAccessor.HttpContext == null ||
                        _httpContextAccessor.HttpContext.User.Identity.Name == null)
                    ? "?"
                    : _httpContextAccessor.HttpContext.User.Identity.Name,
                LogParameters = logParameters
            };

            return logDetailWithException;
        }
    }
}
