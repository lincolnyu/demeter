using System.Collections.Generic;
using System.Linq;

namespace Demeter.Entities.Ingredients
{
    public static class SyntaxTreeHelper
    {
        public static IEnumerable<IList<SyntaxLeaf>> Traverse(this SyntaxNode node)
        {
            var br = node as SyntaxBranch;
            if (br != null)
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
            var leaf = node as SyntaxLeaf;
            if (leaf != null)
            {
                yield return new List<SyntaxLeaf> { leaf };
            }
        }
    }
}
