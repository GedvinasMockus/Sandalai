using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Logging
{
    public class InfoLogger : Logger
    {
        public InfoLogger(Logger nextLogger) : base(nextLogger)
        {
            base.nextLogger = nextLogger;
        }

        public override void process(Log Log)
        {
            if(Log.LogLevel == "INFO")
            {
                string timestamp = DateTime.Now.ToString("yyyy_MM_dd-HH:mm:ss: ");
                string fileName = "Logs/" + timestamp.Split("-")[0] + "_Logs.txt";
                timestamp = timestamp.Split("-")[1];
                string message = timestamp + "[INFO] " + Log.Message;
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
