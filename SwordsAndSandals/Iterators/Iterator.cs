using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Iterators
{
    public abstract class Iterator
    {
        public abstract Button getNext();
        public abstract bool hasMore();
    }
}
