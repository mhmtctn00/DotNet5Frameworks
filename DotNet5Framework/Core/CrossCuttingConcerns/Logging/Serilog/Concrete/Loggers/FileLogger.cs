using Serilog;
using System.IO;
using System;
using Serilog.Events;
using Serilog.Core;
using Newtonsoft.Json;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.Messages;
using Microsoft.Extensions.Configuration;
using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Concrete.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        private readonly string LogFolderPath = @$"C:\LogFiles\"; // Folder path
        //private readonly string LogFolderPath = Directory.GetParent(Directory.GetCurrentDirectory()) + @"\LogFiles\"; // API's Parent Path = YourServicePlatform(Root Folder) + /LogFiles/
        private static readonly object _lock = new object();
        private Logger _logger;
        JsonSerializerSettings settings;
        public FileLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            var logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
                                .Get<FileLogConfiguration>() ??
                            throw new Exception(SerilogMessages.NullOptionsMessage);
            var logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + logConfig.FolderPath, ".txt");

            Logger = new LoggerConfiguration()
                .WriteTo.File(
                    logFilePath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null,
                    fileSizeLimitBytes: 5000000,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}