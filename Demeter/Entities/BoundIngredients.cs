using System.Collections.Generic;

namespace Demeter.Entities
{
    public class BoundIngredients
    {
        public readonly List<Ingredient> Ingredients = new List<Ingredient>();

        public Component Parent { get; set; }
    }
}
