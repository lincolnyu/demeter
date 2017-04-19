using Demeter.Entities;
using System.Collections.Generic;

namespace Demeter.Management
{
    /// <summary>
    ///  A collection of recipes
    /// </summary>
    public class Repertoire
    {
        public readonly Dictionary<string, Recipe> Recipes = new Dictionary<string, Recipe>();
    }
}
