using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SwordsAndSandals.Logging
{
    public class WarningLogger : Logger
    {
        public WarningLogger(Logger nextLogger) : base(nextLogger)
        {
            base.nextLogger = nextLogger;
        }

        public override void process(Log Log)
        {
            if (Log.LogLevel == "WARNING")
            {
                string timestamp = DateTime.Now.ToString("yyyy_MM_dd-HH:mm:ss: ");
                string fileName = timestamp.Split("-")[0] + "_Logs.txt";
                timestamp = timestamp.Split("-")[1];
                string message = timestamp + "[WARNING] " + Log.Message;
                Debug.WriteLine(message);
                string currentDirectory = Directory.GetCurrentDirectory();
                string filePath = Path.Combine(currentDirectory, fileName);
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(message);
                }
            }
            else
            {
                nextLogger.process(Log);
            }
        }
    }
}
