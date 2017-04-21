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
            public double Serve;
        }

        public delegate double GetServeCb(Recipe recipe, int alt, int level);

        /// <summary>
        ///  Returns all the possible sets of dishes that can be made out of the stock and the recipes
        /// </summary>
        /// <param name="dispenser">The unit that creates dishes by taking ingredients off from stock</param>
        /// <param name="stock">The stock that provides the ingredients</param>
        /// <param name="recipes">All recipes</param>
        /// <param name="start">The recipe to start searching from for this traverse from the recipes</param>
        /// <param name="getServe">The method that returns the quantity for the current dispensing</param>
        /// <param name="mode">The mode in which the ingredients are dispensed</param>
        /// <param name="allowDuplicates">Whether allow recipes to be used multiple times</param>
        /// <returns>All the possible sets enumerated, each set is in the form of an enumeration of the dishes it contains</returns>
        public static IEnumerable<IEnumerable<Dish>> Traverse(this IngredientDispenser dispenser, Stock stock, IList<Recipe> recipes, int start, GetServeCb getServe, IngredientDispenser.DispenseModes mode, bool allowDuplicates = false, int level = 0)
        {
            for (var i = start; i < recipes.Count; i++)
            {
                var recipe = recipes[i];
                var altCount = dispenser.AltCount(recipe);
                for (var alt = 0; alt < altCount; alt++)
                {
                    var serve = getServe(recipe, alt, level);
                    var dispensed = dispenser.Dispense(stock, recipe, serve, alt, mode);
                    if (dispensed.Any())
                    {
                        var res = dispenser.Traverse(stock, recipes, allowDuplicates ? start : i
                            , getServe, mode, allowDuplicates, level + 1);
                        foreach (var r in res)
                        {
                            yield return Concat(new Dish { Recipe = recipe, Alt = alt, Serve = serve }, r);
                        }
                        dispenser.Restore(stock, dispensed);
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<Dish>> Traverse(this IngredientDispenser dispenser, Stock stock, IList<Recipe> recipes, int start, double serve, IngredientDispenser.DispenseModes mode, bool allowDuplicates = false)
            => dispenser.Traverse(stock, recipes, start, (r, a, l) => serve, mode, allowDuplicates);

        private static IEnumerable<Dish> Concat(Dish newHead, IEnumerable<Dish> dishes)
        {
            yield return newHead;
            foreach (var d in dishes) yield return d;
        }
    }
}