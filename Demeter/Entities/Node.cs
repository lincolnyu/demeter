using System.Collections.Generic;
using Demeter.Abstract;
using System.Linq;

namespace Demeter.Entities
{
    public abstract class Node : Processor
    {
        public IList<Item> Inputs { get; protected set; }
        public IList<Item> Outputs { get; protected set; }

        public IList<Node> Nexts { get; } = new List<Node>();

        public bool IsEndNode => Nexts.Count == 0;

        protected abstract bool InputsReady();

        protected abstract void AssignOutputsToNexts();

        public virtual bool PushIfReady(IProcessingContext ctx)
        {
            if (!InputsReady()) return false;
            Outputs = Process(Inputs, ctx).ToList();
            if (!IsEndNode)
            {
                AssignOutputsToNexts();
            }
            return true;
        }
    }
}
