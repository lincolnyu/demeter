using System.Collections.Generic;
using Demeter.Inventory;

namespace Demeter.Elemental
{
    /// <summary>
    ///  The type of an item
    /// </summary>
    public abstract class ItemType
    {
        #region Constructors

        /// <summary>
        ///  Instantiates an ItemType object
        /// </summary>
        protected ItemType()
        {
            Instances = new HashSet<Item>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///  The name of the type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  The manager
        /// </summary>
        public KitchenManager Owner { get; set; }

        /// <summary>
        ///  All instances of the type
        /// </summary>
        public ISet<Item> Instances { get; private set; } 

        #endregion

        #region Methods

        /// <summary>
        ///  Instantiates an item of this type
        /// </summary>
        /// <returns></returns>
        public abstract Item Instantiate();

        /// <summary>
        ///  current decay rate (ratio of decrease to current quality per day)
        /// </summary>
        /// <param name="storageType">The condition of storage</param>
        /// <returns>The decay rate</returns>
        public abstract double GetDecayRate(StorageType storageType);

        #endregion
    }
}
