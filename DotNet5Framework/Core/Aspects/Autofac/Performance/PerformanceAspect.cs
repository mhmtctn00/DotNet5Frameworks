using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Performance.Database;
using Core.CrossCuttingConcerns.Performance.File;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;
        //DatabasePerformance _databasePerformance = new DatabasePerformance();
        FilePerformance _filePerformance = new FilePerformance();

        public PerformanceAspect(int interval)
        {            
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();            
        }


        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }
        string className, methodName, time;
        protected override void OnAfter(IInvocation invocation)
        {
           
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            { 
                Debug.WriteLine($"Performance : {invocation.TargetType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
                className = invocation.TargetType.FullName.ToString();
                methodName = invocation.Method.Name.ToString();
                time = _stopwatch.Elapsed.TotalSeconds.ToString();
                //_databasePerformance.Add(className, methodName, time);
                _filePerformance.WriteToPerformanceFile(className, methodName, time);
            }            
            _stopwatch.Reset();
        }
        
    }
}
