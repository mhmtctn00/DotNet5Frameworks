using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Performance.File
{
    public class FilePerformance
    {
        private readonly string fileAddress = @"C:\LogFiles\PerformanceFiles\Performance.txt";
        public static object _locked = new object();
        public FilePerformance()
        {
            ControlDirectories(fileAddress);
        }
        private static void ControlDirectories(string fileAddress)
        {
            if (!Directory.Exists(fileAddress))
            {
                Directory.CreateDirectory(@"C:\LogFiles\PerformanceFiles");
            }
        }
        public void WriteToPerformanceFile(string className, string methodName, string time)
        {
            const string quote = "\"";
            try
            {
                lock (_locked)
                {
                    using (FileStream fs = new FileStream(fileAddress, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine("{" + quote + "ClassName" + quote + ":" + quote + className + quote + "," + quote + "MethodName" + quote + ":" + quote + methodName + quote + "," + quote + "Time" + quote + ":" + quote + time + quote + "}");
                            sw.Flush();
                            sw.Close();
                        }
                        fs.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
