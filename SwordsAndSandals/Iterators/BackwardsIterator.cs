using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Iterators
{
    public class BackwardsIterator : Iterator
    {
        private ButtonAggregate aggregate;
        private int index;
        public BackwardsIterator(ButtonAggregate aggregate)
        {
            this.aggregate = aggregate;
            this.index = aggregate.Count() - 1;
        }
        public override Button getNext()
        {
            this.index--;
            return this.aggregate.Get(this.index+1);
        }

        public override bool hasMore()
        {
            return this.index >= 0;
        }
    }
}
