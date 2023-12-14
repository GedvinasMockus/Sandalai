using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Iterators
{
    public class ForwardsIterator : Iterator
    {
        private ButtonAggregate aggregate;
        private int index;
        public ForwardsIterator(ButtonAggregate aggregate)
        {
            this.aggregate = aggregate;
            this.index = 0;
        }
        public override Button getNext()
        {
            this.index++;
            return aggregate.Get(this.index-1);
        }

        public override bool hasMore()
        {
            return aggregate.Count() > this.index;
        }
    }
}
