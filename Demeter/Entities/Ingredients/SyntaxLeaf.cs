namespace Demeter.Entities.Ingredients
{
    public abstract class SyntaxLeaf : SyntaxNode
    {
        public StuffType Type { get; set; }

        public abstract WeightRange GetWeight(double main);
    }
}
