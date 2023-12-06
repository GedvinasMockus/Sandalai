using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Logging
{
    public class DebugLogger : Logger
    {
        public DebugLogger(Logger nextLogger) : base(nextLogger)
        {
            base.nextLogger = nextLogger;
        }

        public override void process(Log Log)
        {
            if (Log.LogLevel == "DEBUG")
            {
                string timestamp = DateTime.Now.ToString("yyyy_MM_dd-HH:mm:ss: ");
                timestamp = timestamp.Split("-")[1];
                string message = timestamp + "[DEBUG] " + Log.Message;
                Debug.WriteLine(message);
            }
            else
            {
                nextLogger.process(Log);
            }
        }
    }
}
