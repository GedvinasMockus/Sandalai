using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Logging
{
    public class LogHandler
    {
        public Logger chain;

        public LogHandler()
        {
            this.chain = new ErrorLogger(new InfoLogger(new WarningLogger(new DebugLogger(null))));
        }

        public void process(Log Log)
        {
            Debug.WriteLine("In Log Handler process: " + this.chain);
            chain.process(Log);
        }
    }
}
