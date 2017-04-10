using Demeter.Abstract;
using System.Collections.Generic;
using System;

namespace Demeter.Entities
{
    public class Stock : Node
    {
        public Dictionary<StuffType, Stuff> Items = new Dictionary<StuffType, Stuff>();

        public override Node Clone()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IItem> Process(IEnumerable<IItem> items, IProcessingContext ctx)
        {
            throw new NotImplementedException();
        }

        protected override bool InputsReady(IProcessingContext ctx)
        {
            throw new NotImplementedException();
        }

        protected override void SynchronizeOutputs(IProcessingContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}
