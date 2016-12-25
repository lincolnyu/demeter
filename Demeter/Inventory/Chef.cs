using System;
using Demeter.Elemental;

namespace Demeter.Inventory
{
    /// <summary>
    ///  An entity that's able to cook
    /// </summary>
    public class Chef : Processor
    {
        #region Properties

        /// <summary>
        ///  The recipe to work on
        /// </summary>
        public Recipe Recipe { get; set; }

        /// <summary>
        ///  The currently applied quantity factor
        /// </summary>
        public double QuantityFactor { get; set; }

        #endregion

        #region Methods

        #region Processor members

        public override TimeSpan Process()
        {
            throw new NotImplementedException();
        }

        public override void Output()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}
