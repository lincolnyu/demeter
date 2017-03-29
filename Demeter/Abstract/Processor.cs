using System;
using System.Collections.Generic;

namespace Demeter.Abstract
{
    public abstract class Processor : Item
    {
        public override bool IsNull => false;

        public override Item Clone() { throw new NotSupportedException(); }

        public abstract IEnumerable<Item> Process(IEnumerable<Item> items, IProcessingContext tp);
    }
}
