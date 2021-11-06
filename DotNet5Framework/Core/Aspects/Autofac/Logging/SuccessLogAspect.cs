﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Logging
{
    public class SuccessLogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerService;

        public SuccessLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }

            _loggerService = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            LogDetail logDetailWithException = GetLogDetail(invocation);
            _loggerService.Info(logDetailWithException);
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
                LogParameters = logParameters
            };

            return logDetailWithException;
        }
    }
}
