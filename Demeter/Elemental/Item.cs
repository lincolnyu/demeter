using System;

namespace Demeter.Elemental
{
    /// <summary>
    ///  Represents an abstract item
    /// </summary>
    public abstract class Item
    {
        #region Properties

        /// <summary>
        ///  The type of this ingredient
        /// </summary>
        public ItemType Type { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///  Ages the item by the specified time
        /// </summary>
        /// <param name="t">The time to age</param>
        public abstract void Age(TimeSpan t);

        /// <summary>
        ///  Copies from another item
        /// </summary>
        /// <param name="other">another item to copy from</param>
        public virtual void CopyFrom(Item other)
        {
            Type = other.Type;
        }

        #endregion
    }
}
