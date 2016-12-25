using System;
using Demeter.Elemental;

namespace Demeter.Inventory
{
    /// <summary>
    ///  Fresh food (ingredient(s) or made dish) 
    /// </summary>
    public class Freshie : Item
    {
        #region Properties
       
        /// <summary>
        ///  Current storage condition
        /// </summary>
        /// <remarks>
        ///  NOTE storage should be changed only by processors
        ///  which means during an aging period the storage should remain the same
        /// </remarks>
        public StorageType CurrentStorage { get; set; }

        /// <summary>
        ///  The time at which the initial quality was assessed 
        /// (and also probably but not necessarily when the ingredient was precured)
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        ///  A value between 0 and 1 indicating initial freshness
        /// </summary>
        public double InitialFreshness { get; set; }

        /// <summary>
        ///  A value between 0 and 1 indicating current freshness
        /// </summary>
        public double CurrentFreshness { get; set; }

        /// <summary>
        ///  Trading unit price (per KG)
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        ///  Appx quantity in KG (net or gross depending on applicability)
        /// </summary>
        public double Quantity { get; set; }

        #endregion

        #region Methods

        #region Item members

        /// <summary>
        ///  Ages the item by the specified time
        /// </summary>
        /// <param name="t">The time to age</param>
        public override void Age(TimeSpan t)
        {
            // simple exponential aging
            var decayRate = GetDecayRate(CurrentStorage);
            var decay = Math.Pow(decayRate, t.TotalHours);
            CurrentFreshness *= decay;
        }

        /// <summary>
        ///  Copies from another item
        /// </summary>
        /// <param name="other">another item to copy from</param>
        public override void CopyFrom(Item other)
        {
            base.CopyFrom(other);
            var o = other as Freshie;
            if (o == null)
            {
                return;
            }
            CurrentStorage = o.CurrentStorage;
            Start = o.Start;
            InitialFreshness = o.InitialFreshness;
            CurrentFreshness = o.CurrentFreshness;
            Price = o.Price;
            Quantity = o.Quantity;
        }

        #endregion

        /// <summary>
        ///  current decay rate (ratio of decrease to current quality per day)
        /// </summary>
        /// <param name="storageType">The condition of storage</param>
        /// <returns>The decay rate</returns>
        public virtual double GetDecayRate(StorageType storageType)
        {
            return Type.GetDecayRate(storageType);
        }

        #endregion
    }
}
