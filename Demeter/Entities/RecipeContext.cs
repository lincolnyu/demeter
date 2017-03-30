using System;
using Demeter.Abstract;
using System.Collections.Generic;

namespace Demeter.Entities
{
    public class RecipeContext : IProcessingContext
    {
        public delegate IProcessingContext CreateContext();

        public virtual string Result
        {
            get; set;
        }

        public virtual TimeSpan? Time
        {
            get; set;
        }

        public readonly Dictionary<string, CreateContext> SubContextCreators = new Dictionary<string, CreateContext>();

        public readonly Dictionary<Recipe, IProcessingContext> SubContexts = new Dictionary<Recipe, IProcessingContext>();

        public virtual IProcessingContext GetContext(Recipe subnode)
        {
            IProcessingContext ctx;
            if (!SubContexts.TryGetValue(subnode, out ctx))
            {
                ctx = SubContextCreators[subnode.Name]();
                SubContexts[subnode] = ctx;
            }
            return ctx;
        }
    }
}
