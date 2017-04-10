using Demeter.Entities.Ingredients;
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

        private Dictionary<Recipe, IList<IList<SyntaxLeaf>>> RecipeCache = new Dictionary<Recipe, IList<IList<SyntaxLeaf>>>();

        public bool Dispense(Stock stock, Recipe recipe, double serve, int altnumber, DispenseModes mode)
        {
            throw new System.NotImplementedException();
        }

        private IList<IList<SyntaxLeaf>> Flatten(Recipe recipe)
        {
            IList<IList<SyntaxLeaf>> result;
            if (!RecipeCache.TryGetValue(recipe, out result))
            {
                result = recipe.Root.Traverse().ToList();
                RecipeCache[recipe] = result;
            }
            return result;
        }

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
