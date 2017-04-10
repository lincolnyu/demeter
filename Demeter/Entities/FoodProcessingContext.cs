using System;
using Demeter.Abstract;

namespace Demeter.Entities
{
    public class FoodProcessingContext : IProcessingContext
    {
        public enum ProcessingModes
        {
            Greedy,
            MatchOnly
        }

        public delegate FoodProcessingContext CreateContext();

        public virtual string Result
        {
            get; set;
        }

        public virtual TimeSpan? Time
        {
            get; set;
        }

        public Recipe Recipe { get; set; }

        public ProcessingModes Mode { get; set; }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
