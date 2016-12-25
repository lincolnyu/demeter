using Demeter.Elemental;

namespace Demeter.Inventory
{
    /// <summary>
    ///  An ingredient in a recipe
    /// </summary>
    public class Ingredient
    {
        #region Properties

        /// <summary>
        ///  Item type
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        ///  How to pick from existing items
        /// </summary>
        public PickPreferences Pick { get; set; }

        /// <summary>
        ///  Typical quanity
        /// </summary>
        public double Quantity { get; set; }

        #endregion
    }
}
