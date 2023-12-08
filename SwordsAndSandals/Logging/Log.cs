using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Logging
{
    public class Log
    {
        public string LogLevel;
        public string Message;

        public Log(String LogLevel, String Message)
        {
            this.LogLevel = LogLevel;
            this.Message = Message;
        }
    }
}
