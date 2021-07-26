using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public abstract class LoggerServiceBase
    {
        public abstract void Info(object logMessage);
        public abstract void Error(object logMessage);
        public abstract void Fatal(object logMessage);
        public abstract void Warning(object logMessage);
        public abstract void Debug(object logMessage);
    }
}
