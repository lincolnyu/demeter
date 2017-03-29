namespace Demeter.Abstract
{
    public abstract class Item
    {
        /// <summary>
        ///  Whether the item is completely consumed and turned into something else
        /// </summary>
        /// <remarks>
        ///  In this case any entity that references this item should be aware and 
        ///  take due actions (like de-referencing it)
        /// </remarks>
        public abstract bool IsNull { get; }

        public abstract Item Clone();
    }
}
