using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Iterators
{
    public abstract class Aggregate
    {
        public abstract Iterator CreateIterator(String type);
    }
}
