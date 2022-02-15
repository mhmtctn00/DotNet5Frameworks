using System;
using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MySQL;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class MySqlLogger : LoggerServiceBase
    {
        public MySqlLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:MySqlConfiguration")
                                .Get<MsSqlConfiguration>() ??
                            throw new Exception(SerilogMessages.NullOptionsMessage);

            Logger = new LoggerConfiguration().WriteTo
                .MySQL(logConfig.ConnectionString, tableName: "Logs")
                .CreateLogger();
        }
    }
}