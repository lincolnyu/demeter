﻿using System;
using System.Collections.Generic;
using Demeter.Abstract;

namespace Demeter.Entities
{
    public class FoodProcessor : Node
    {
        #region CompositeNode members

        #region Node mebers

        #region IClonable members

        public override Node Clone()
        {
            return new FoodProcessor
            {
                Name = Name
            };
            // TODO other fields
        }

        #endregion

        protected override bool InputsReady(IProcessingContext ctx)
        {
            var rctx = (FoodProcessingContext)ctx;
            
            throw new NotImplementedException();
        }

        protected override void SynchronizeOutputs(IProcessingContext ctx)
        {
            var rctx = (FoodProcessingContext)ctx;
            throw new NotImplementedException();
        }

        public override IEnumerable<IItem> Process(IEnumerable<IItem> items, IProcessingContext ctx)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        public string Name { get; set; }
    }
}
