using Demeter.Entities.Ingredients;
using QSharp.Scheme.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Demeter.Entities
{
    public class IngredientDispenser
    {
        public enum DispenseModes
        {
            Ideal,
            Minimum
        }

        private Cache<Recipe, IList<IList<SyntaxLeaf>>> _recipeTraversalCache;

        public IngredientDispenser(uint cacheSize)
        {
            var policy = new LruCachePolicy<Recipe, IList<IList<SyntaxLeaf>>>(cacheSize);
            _recipeTraversalCache = new Cache<Recipe, IList<IList<SyntaxLeaf>>>(GetTraversal, policy.UpdateCache);
        }

        public bool Dispense(Stock stock, Recipe recipe, double serve, int altnumber, DispenseModes mode)
        {
            throw new System.NotImplementedException();
        }

        private IList<IList<SyntaxLeaf>> GetTraversal(Recipe key) => key.Root.Traverse().ToList();
        
        private IList<IList<SyntaxLeaf>> Flatten(Recipe recipe) => _recipeTraversalCache.Get(recipe);

        private bool Dispense(Stock stock, IList<SyntaxLeaf> leaves, double serve, DispenseModes mode)
        {
            foreach (var leaf in leaves)
            {
                Stuff stuff;
                if (!stock.Items.TryGetValue(leaf.Type, out stuff))
                {
                    return false;
                }
                var w = leaf.GetWeight(serve);
                if (mode == DispenseModes.Ideal && stuff.Quantity < w.IdealWeight || mode == DispenseModes.Minimum && stuff.Quantity < w.MinWeight) return false;
            }
            foreach (var leaf in leaves)
            {
                var stuff = stock.Items[leaf.Type];
                var w = leaf.GetWeight(serve);
                if (mode == DispenseModes.Ideal) stuff.Quantity -= w.IdealWeight;
                else if (mode == DispenseModes.Minimum) stuff.Quantity -= w.MinWeight;
            }
            return true;
        }

    }
}
