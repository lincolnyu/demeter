using System.Collections.Generic;

namespace Demeter.Abstract
{
    public abstract class CompositeNode : Node
    {
        /// <summary>
        ///  The front nodes that receive the inputs of this node
        /// </summary>
        public IList<Node> Firsts { get; } = new List<Node>();

        /// <summary>
        ///  Assigns the current `Inputs' to the `Firsts' or returns as items that passes through
        /// </summary>
        /// <remarks>
        ///  Passthrough should take the processing time into 
        /// </remarks>
        /// <param name="tp"></param>
        /// <returns></returns>
        protected abstract IEnumerable<IItem> AssignInputsToFirstsOrPassthrough(IProcessingContext tp);

        /// <summary>
        ///  Extract the processing context for the specified internal node of this composite node
        /// </summary>
        /// <param name="ctx">The processing context</param>
        /// <param name="node">The node to the extract processing context for</param>
        /// <returns>The processing context</returns>
        protected abstract IProcessingContext ExtractContext(IProcessingContext ctx, Node node);

        public override IEnumerable<IItem> Process(IEnumerable<IItem> items, IProcessingContext ctx)
        {
            var passthru = AssignInputsToFirstsOrPassthrough(ctx);
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
                var nctx = ExtractContext(ctx, n);
                if (n.PushIfReady(nctx))
                {
                    if (n.IsTerminal)
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
