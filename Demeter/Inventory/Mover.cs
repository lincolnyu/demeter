using System;
using System.Collections.Generic;
using Demeter.Elemental;
using Demeter.Helpers;

namespace Demeter.Inventory
{
    /// <summary>
    ///  Entity that moves an item from one storage to another
    /// </summary>
    public class Mover : Processor
    {
        #region Fields

        /// <summary>
        ///  The goods being moved
        /// </summary>
        private readonly ISet<Item> _itemsInProgress = new HashSet<Item>();

        #endregion

        #region Properties

        /// <summary>
        ///  The source storage
        /// </summary>
        public StorageType Source { get; set; }

        /// <summary>
        ///  The target storage
        /// </summary>
        public StorageType Target { get; set; }

        /// <summary>
        ///  Type of the item to move (has to be associated with its type and further with the manager)
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        ///  How to choose among items of same type
        /// </summary>
        public PickPreferences Pick { get; set; }

        /// <summary>
        ///  Quantity to move
        /// </summary>
        public double Quantity { get; set; }

        #endregion

        #region Methods

        #region Processor members

        /// <summary>
        ///  Takes whatever is available from the manager, conducts the processing and returns the duration
        /// </summary>
        /// <returns>The duration the process takes</returns>
        public override TimeSpan Process()
        {
            switch (Pick)
            {
                case PickPreferences.FreshestFirst:
                    ItemType.PickFreshestFirst(Quantity, x => x.CurrentStorage != Target, _itemsInProgress);
                    break;
                case PickPreferences.OldestFirst:
                    ItemType.PickOldestFirst(Quantity, x => x.CurrentStorage != Target, _itemsInProgress);
                    break;
                case PickPreferences.Random:
                    ItemType.PickRandomly(Quantity, x => x.CurrentStorage != Target, _itemsInProgress);
                    break;
            }

            InProgress = _itemsInProgress.Count > 0;
            return TimeSpan.Zero;
        }

        /// <summary>
        ///  Outputs back to the manager
        /// </summary>
        public override void Output()
        {
            foreach (var item in _itemsInProgress)
            {
                var freshie = item as Freshie;
                if (freshie != null)
                {
                    Source.Remove(freshie);
                    Source.Add(freshie);
                }
                ItemType.Instances.Add(item);
            }
            InProgress = false;
        }

        #endregion

        #endregion
    }
}
