using Demeter.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Demeter.Management
{
    public static class TraversalHelper
    {
        public class Dish
        {
            public Recipe Recipe;
            public int Alt;
        }

        public static IEnumerable<IEnumerable<Dish>> Traverse(this IngredientDispenser dispenser, Stock stock, IList<Recipe> recipes, int start, double serve, IngredientDispenser.DispenseModes mode)
        {
            for (var i = start; i < recipes.Count; i++)
            {
                var recipe = recipes[i];
                var altCount = dispenser.AltCount(recipe);
                for (var alt = 0; alt < altCount; alt++)
                {
                    var dispensed = dispenser.Dispense(stock, recipe, serve, alt, mode);
                    if (dispensed.Any())
                    {
                        var res = dispenser.Traverse(stock, recipes, i, serve, mode);
                        foreach (var r in res)
                        {
                            yield return Concat(new Dish { Recipe = recipe, Alt = alt }, r);
                        }
                        dispenser.Restore(stock, dispensed);
                    }
                }
            }
        }

        private static IEnumerable<Dish> Concat(Dish newHead, IEnumerable<Dish> dishes)
        {
            yield return newHead;
            foreach (var d in dishes) yield return d;
        }
    }
}