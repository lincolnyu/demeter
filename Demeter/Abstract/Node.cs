using System.Collections.Generic;
using System.Linq;

namespace Demeter.Abstract
{
    public abstract class Node : IProcessor, IClonable<Node>
    {
        #region IProcessor members

        public abstract IEnumerable<IItem> Process(IEnumerable<IItem> items, IProcessingContext ctx);

        #endregion

        #region IClonable members

        public abstract Node Clone();

        #endregion

        /// <summary>
        ///  Buffer of inputs from upstream nodes
        /// </summary>
        public IList<IItem> Inputs { get; protected set; }
        
        /// <summary>
        ///  Buffer of outputs produced by this node
        /// </summary>
        public IList<IItem> Outputs { get; protected set; }

        /// <summary>
        ///  Nodes that are incidental to this node
        /// </summary>
        public IList<Node> Nexts { get; } = new List<Node>();

        /// <summary>
        ///  If this node is terminal node
        /// </summary>
        public virtual bool IsTerminal => Nexts.Count == 0;

        protected abstract bool InputsReady(IProcessingContext ctx);

        /// <summary>
        ///  This synchronizes all the to the context and the current time and assigns
        ///  outputs to Nexts if Nexts are available (this is not terminal node)
        /// </summary>
        /// <param name="ctx">The processing context</param>
        protected abstract void SynchronizeOutputs(IProcessingContext ctx);

        /// <summary>
        ///  If Inputs are all ready, processes the inputs and pushes the output to the downstream
        /// </summary>
        /// <param name="ctx">The processing context</param>
        /// <returns>True if inputs are ready and processing is performed or false</returns>
        public bool PushIfReady(IProcessingContext ctx)
        {
            if (!InputsReady(ctx))
            {
                return false;
            }

            Outputs = Process(Inputs, ctx).ToList();
            SynchronizeOutputs(ctx);
            return true;
        }
    }
}
