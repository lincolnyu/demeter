using System.Collections.Generic;
using Demeter.Abstract;

namespace Demeter.Entities
{
    public abstract class Recipe : Node
    {
        public IList<Node> Firsts { get; } = new List<Node>();

        protected abstract IEnumerable<Item> AssignInputsToFirstsOrPassthrough();

        protected abstract IProcessingContext ExtractContext(IProcessingContext tp, Node node);

        public override IEnumerable<Item> Process(IEnumerable<Item> items, IProcessingContext tp)
        {
            var passthru = AssignInputsToFirstsOrPassthrough();
            foreach (var p in passthru)
            {
                yield return p;
            }
            var q = new Queue<Node>();
            foreach (var f in Firsts)
            {
                q.Enqueue(f);
            }
            while (q.Count > 0)
            {
                var n = q.Dequeue();
                var ctx = ExtractContext(tp, n);
                if (n.PushIfReady(ctx))
                {
                    if (n.IsEndNode)
                    {
                        foreach (var o in n.Outputs)
                        {
                            yield return o;
                        }
                    }
                    else
                    {
                        foreach (var nxt in n.Nexts)
                        {
                            q.Enqueue(nxt);
                        }
                    }
                }
                else
                {
                    q.Enqueue(n);
                }
            }
        }
    }
}
