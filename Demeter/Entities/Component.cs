using System.Collections.Generic;

namespace Demeter.Entities
{
    public class Component
    {
        public readonly List<BoundIngredients> Alternatives = new List<BoundIngredients>();

        public Recipe Parent { get; set; }
    }
}
