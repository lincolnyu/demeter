using System;
using Demeter.Abstract;

namespace Demeter.Entities
{
    public class RecipeContext : IProcessingContext
    {
        public delegate RecipeContext CreateContext();

        public virtual string Result
        {
            get; set;
        }

        public virtual TimeSpan? Time
        {
            get; set;
        }
        
        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
