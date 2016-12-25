using System;
using System.Collections.Generic;
using System.Linq;
using Demeter.Elemental;
using Demeter.Inventory;

namespace Demeter.Helpers
{
    /// <summary>
    ///  Helps you pick
    /// </summary>
    public static class PickHelper
    {
        #region Methods

        /// <summary>
        ///  Returns the total quanity of items of the specified type and with freshness above the given minimum
        /// </summary>
        /// <param name="type">The type of the items</param>
        /// <param name="predicate">The criteria to satisfy</param>
        /// <returns>The total quantity</returns>
        public static double GetTotalQuanity(this ItemType type, Predicate<Freshie> predicate)
        {
            return type.Instances.Cast<Freshie>().Where(x=>predicate(x)).Sum(x => x.Quantity);
        }

        /// <summary>
        ///  Picks freshest first
        /// </summary>
        /// <param name="type">The type of the items to pick</param>
        /// <param name="quantity">The quanity</param>
        /// <param name="predicate">The criteria to satisfy</param>
        /// <param name="picked">The picked items</param>
        public static void PickFreshestFirst(this ItemType type, double quantity, Predicate<Freshie> predicate, ISet<Item> picked)
        {
            var ordered = type.Instances.Cast<Freshie>().OrderBy(x => x.CurrentFreshness).ToList();
            foreach (var o in ordered.Where(o => predicate(o)))
            {
                if (o.Quantity <= quantity)
                {
                    picked.Add(o);
                    type.Instances.Remove(o);
                    quantity -= o.Quantity;
                }
                else
                {
                    var n = (Freshie)type.Instantiate();
                    n.CopyFrom(o);
                    n.Quantity = quantity;
                    picked.Add(n);
                    o.Quantity -= quantity;
                    break;
                }
            }
        }

        /// <summary>
        ///  Picks oldest first
        /// </summary>
        /// <param name="type">The type of the items to pick</param>
        /// <param name="quantity">The quanity</param>
        /// <param name="predicate">The criteria to satisfy</param>
        /// <param name="picked">The picked items</param>
        public static void PickOldestFirst(this ItemType type, double quantity, Predicate<Freshie> predicate, ISet<Item> picked)
        {
            var ordered = type.Instances.Cast<Freshie>().OrderByDescending(x => x.CurrentFreshness).ToList();
            foreach (var o in ordered.Where(o => predicate(o)))
            {
                if (o.Quantity <= quantity)
                {
                    picked.Add(o);
                    type.Instances.Remove(o);
                    quantity -= o.Quantity;
                }
                else
                {
                    var n = (Freshie)type.Instantiate();
                    n.CopyFrom(o);
                    n.Quantity = quantity;
                    picked.Add(n);
                    o.Quantity -= quantity;
                    break;
                }
            }
        }

        /// <summary>
        ///  Picks randomly
        /// </summary>
        /// <param name="type">The type of the items to pick</param>
        /// <param name="quantity">The quanity</param>
        /// <param name="predicate">The criteria to satisfy</param>
        /// <param name="picked">The picked items</param>
        public static void PickRandomly(this ItemType type, double quantity, Predicate<Freshie> predicate, ISet<Item> picked)
        {
            foreach (var o in type.Instances.Cast<Freshie>().Where(o=>predicate(o)))
            {
                if (o.Quantity <= quantity)
                {
                    picked.Add(o);
                    type.Instances.Remove(o);
                    quantity -= o.Quantity;
                }
                else
                {
                    var n = (Freshie)type.Instantiate();
                    n.CopyFrom(o);
                    n.Quantity = quantity;
                    picked.Add(n);
                    o.Quantity -= quantity;
                    break;
                }
            }
        }

        #endregion
    }
}
