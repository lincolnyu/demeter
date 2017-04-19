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

        public class Usage
        {
            public SyntaxLeaf Ingredient { get; set; }
            public double Weight { get; set; }
        }

        private Cache<Recipe, IList<IList<SyntaxLeaf>>> _recipeTraversalCache;

        public IngredientDispenser(uint cacheSize)
        {
            var policy = new LruCachePolicy<Recipe, IList<IList<SyntaxLeaf>>>(cacheSize);
            _recipeTraversalCache = new Cache<Recipe, IList<IList<SyntaxLeaf>>>(GetTraversal, policy.UpdateCache);
        }

        public int AltCount(Recipe recipe)
        {
            var flat = Flatten(recipe);
            return flat.Count;
        }

        public IEnumerable<Usage> Dispense(Stock stock, Recipe recipe, double serve, int altnumber, DispenseModes mode)
        {
            var flat = Flatten(recipe);
            var alt = flat[altnumber];
            return Dispense(stock, alt, serve, mode);
        }

        public void Restore(Stock stock, IEnumerable<Usage> usages)
        {
            foreach (var usage in usages)
            {
                var leaf = usage.Ingredient;
                var stuff = stock.Items[leaf.Type];
                stuff.Quantity += usage.Weight;
            }
        }

        private IList<IList<SyntaxLeaf>> GetTraversal(Recipe key) => key.Root.Traverse().ToList();
        
        private IList<IList<SyntaxLeaf>> Flatten(Recipe recipe) => _recipeTraversalCache.Get(recipe);

        private IEnumerable<Usage> Dispense(Stock stock, IList<SyntaxLeaf> leaves, double serve, DispenseModes mode)
        {
            foreach (var leaf in leaves)
            {
                if (!stock.Items.TryGetValue(leaf.Type, out Stuff stuff))
                {
                    yield break;
                }
                var w = leaf.GetWeight(serve);
                if (mode == DispenseModes.Ideal && stuff.Quantity < w.IdealWeight || mode == DispenseModes.Minimum && stuff.Quantity < w.MinWeight) yield break;
            }
            foreach (var leaf in leaves)
            {
                var stuff = stock.Items[leaf.Type];
                var w = leaf.GetWeight(serve);
                double weight = 0.0;
                if (mode == DispenseModes.Ideal) weight = w.IdealWeight;
                else if (mode == DispenseModes.Minimum) weight = w.MinWeight;
                stuff.Quantity -= weight;
                yield return new Usage { Ingredient = leaf, Weight = weight };
            }
        }
    }
}
