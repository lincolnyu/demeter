using System.Collections.Generic;
using System.Linq;

namespace Demeter.Entities.Ingredients
{
    public static class SyntaxTreeHelper
    {
        public static IEnumerable<IList<SyntaxLeaf>> Traverse(this SyntaxNode node)
        {
            if (node is SyntaxBranch br)
            {
                switch (br.Operator)
                {
                    case SyntaxBranch.Operators.And:
                        {
                            var left = Traverse(br.Left);
                            var right = Traverse(br.Right).ToList();
                            foreach (var l in left)
                            {
                                foreach (var r in right)
                                {
                                    yield return l.Concat(r).ToList();
                                }
                            }
                            break;
                        }
                    case SyntaxBranch.Operators.Or:
                        foreach (var r in Traverse(br.Left)) yield return r;
                        foreach (var r in Traverse(br.Right)) yield return r;
                        break;
                }
                yield break;
            }
            else if (node is SyntaxLeaf leaf)
            {
                yield return new List<SyntaxLeaf> { leaf };
            }
        }
    }
}
