using System;
using System.Collections.Generic;

namespace Demeter.Inventory
{
    /// <summary>
    ///  A recipe
    /// </summary>
    public class Recipe
    {
        #region Constructors

        /// <summary>
        ///  Instantiates a Recipe object
        /// </summary>
        public Recipe()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///  All the ingredients
        /// </summary>
        public ISet<Ingredient> Ingredients { get; private set; }

        /// <summary>
        ///  Factor applied to ingredients' quantities to form the mimimum portion
        /// </summary>
        public double MinFactor { get; set; }

        /// <summary>
        ///  Factor applied to ingredients' quantities to form the maximum portion
        /// </summary>
        public double MaxFactor { get; set; }

        /// <summary>
        ///  Base duration
        /// </summary>
        /// <remarks>
        ///  Actual appx duration = BaseDuration + Factor * VariablkeDurationCoeff
        /// </remarks>
        public TimeSpan BaseDuration { get; set; }

        /// <summary>
        ///  Coefficient of the time dependent part of the duration
        /// </summary>
        public TimeSpan VariableDurationCoeff { get; set; }

        #endregion

      
    }
}
