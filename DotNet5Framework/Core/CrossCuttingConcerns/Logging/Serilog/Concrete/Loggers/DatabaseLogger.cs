using Core.CrossCuttingConcerns.Logging.Serilog;
using Serilog.Core;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.IO;
using System;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Concrete.Loggers
{
    public class DatabaseLogger : LoggerServiceBase
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        protected Logger GetLogger()
        {
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string connectionString = "";

            #region From AppSettings.json
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();
            connectionString = configuration.GetConnectionString("ConnectionString");
            #endregion

            return new LoggerConfiguration().WriteTo
                .MySQL(connectionString: connectionString, tableName: "Logs")
                .CreateLogger();
        }


        public override void Info(object logMessage) => GetLogger().Information(JsonConvert.SerializeObject(logMessage, settings));

        public override void Error(object logMessage) => GetLogger().Error(JsonConvert.SerializeObject(logMessage, settings));

        public override void Fatal(object logMessage) => GetLogger().Fatal(JsonConvert.SerializeObject(logMessage, settings));

        public override void Warning(object logMessage) => GetLogger().Warning(JsonConvert.SerializeObject(logMessage, settings));

        public override void Debug(object logMessage) => GetLogger().Debug(JsonConvert.SerializeObject(logMessage, settings));
    }
}