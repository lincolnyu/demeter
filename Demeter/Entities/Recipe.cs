using System;
using System.Collections.Generic;
using Demeter.Abstract;

namespace Demeter.Entities
{
    public class Recipe : CompositeNode
    {
        #region CompositeNode members

        #region Node mebers

        #region BaseItem members

        public override Node Clone()
        {
            return new Recipe();
        }

        #endregion

        protected override bool InputsReady(IProcessingContext ctx)
        {
            throw new NotImplementedException();
        }

        protected override void SynchronizeOutputs(IProcessingContext ctx)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override IEnumerable<IItem> AssignInputsToFirstsOrPassthrough(IProcessingContext tp)
        {
            throw new NotImplementedException();
        }

        protected override IProcessingContext ExtractContext(IProcessingContext ctx, Node node)
        {
            throw new NotImplementedException();
        }

        #endregion

        public string Name { get; set; }
    }
}
