using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Logging
{
    public abstract class Logger
    {
        protected Logger nextLogger;

        public Logger(Logger nextLogger)
        {
            this.nextLogger = nextLogger;
        }

        public abstract void process(Log Log);
    }
}
