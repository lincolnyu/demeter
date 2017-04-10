namespace Demeter.Entities.Ingredients
{
    public class SyntaxBranch : SyntaxNode
    {
        public enum Operators
        {
            And,
            Or
        }

        public Operators Operator { get; set; }
        public SyntaxNode Left { get; set; }
        public SyntaxNode Right { get; set; }
    }
}
