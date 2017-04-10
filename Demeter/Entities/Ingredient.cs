namespace Demeter.Entities
{
    public abstract class Ingredient
    {
        public Ingredient(StuffType type, BoundIngredients parent)
        {
            Type = type;
            Parent = parent;
        }

        public StuffType Type { get; }

        public BoundIngredients Parent { get; }

        public abstract WeightRange GetWeight(double main);
    }
}
