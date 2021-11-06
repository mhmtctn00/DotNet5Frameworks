using Serilog;
using System.IO;
using System;
using Serilog.Events;
using Serilog.Core;
using Newtonsoft.Json;

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
            settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            ControlDirectories(LogFolderPath);
        }

        private static void ControlDirectories(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine(path);
            }

            if (!Directory.Exists(path + @"\Error\"))
            {
                Directory.CreateDirectory(path + @"\Error\");
                Console.WriteLine(path + @"\Error\");
            }

            if (!Directory.Exists(path + @"\Information\"))
            {
                Directory.CreateDirectory(path + @"\Information\");
                Console.WriteLine(path + @"\Information\");
            }

            if (!Directory.Exists(path + @"\Other\"))
            {
                Directory.CreateDirectory(path + @"\Other\");
                Console.WriteLine(path + @"\Other\");
            }
        }

        protected Logger GetLogger()
        { // Create a Logger object that is provided by Serilog
            if (_logger == null)
            {
                lock (_lock) // thread safe, singleton
                {
                    _logger = new LoggerConfiguration()
                                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(error => error.Level == LogEventLevel.Error))
                                .WriteTo.File(string.Format(@"{0}\Error\error-.log", LogFolderPath), LogEventLevel.Error, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, fileSizeLimitBytes: 5000000, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} | {Level}] {Message:lj}{NewLine}{Exception}")

                                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(info => info.Level == LogEventLevel.Information))
                                .WriteTo.File(string.Format(@"{0}\Information\information-.log", LogFolderPath), LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, fileSizeLimitBytes: 5000000, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} | {Level}] {Message:lj}{NewLine}{Exception}")

                                .WriteTo.Logger(lc => lc.Filter.ByExcluding(other => other.Level == LogEventLevel.Error || other.Level == LogEventLevel.Information))
                                .WriteTo.File(string.Format(@"{0}\Other\other-.log", LogFolderPath), LogEventLevel.Warning, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, fileSizeLimitBytes: 5000000, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} | {Level}] {Message:lj}{NewLine}{Exception}")
                                .CreateLogger();
                    // LogEventLevel: Verbose < Debug < Information < Warning < Error < Fatal
                }
            }
            return _logger;
        }

        /*
         * The methods are filled according to FileLogger
         */


        public override void Info(object logMessage) => GetLogger().Information(JsonConvert.SerializeObject(logMessage, settings));

        public override void Error(object logMessage) => GetLogger().Error(JsonConvert.SerializeObject(logMessage, settings));

        public override void Fatal(object logMessage) => GetLogger().Fatal(JsonConvert.SerializeObject(logMessage, settings));

        public override void Warning(object logMessage) => GetLogger().Warning(JsonConvert.SerializeObject(logMessage, settings));

        public override void Debug(object logMessage) => GetLogger().Debug(JsonConvert.SerializeObject(logMessage, settings));

    }
}