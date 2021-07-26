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
        protected Logger GetLogger()
        {
            string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = DotNet5Framework; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            #region From AppSettings.json
            //var configuration = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //     .Build();
            //connectionString = configuration.GetConnectionString("ConnectionString");
            #endregion

            return new LoggerConfiguration().WriteTo
                .MSSqlServer(connectionString: connectionString, tableName: "Logs", autoCreateSqlTable: true)
                .CreateLogger();
        }

        public override void Info(object logMessage) => GetLogger().Information(JsonConvert.SerializeObject(logMessage));

        public override void Error(object logMessage) => GetLogger().Error(JsonConvert.SerializeObject(logMessage));

        public override void Fatal(object logMessage) => GetLogger().Fatal(JsonConvert.SerializeObject(logMessage));

        public override void Warning(object logMessage) => GetLogger().Warning(JsonConvert.SerializeObject(logMessage));

        public override void Debug(object logMessage) => GetLogger().Debug(JsonConvert.SerializeObject(logMessage));
    }
}