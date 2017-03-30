namespace Demeter.Abstract
{
    public interface IItem : IClonable<IItem>
    {
        /// <summary>
        ///  Whether the item is completely consumed and turned into something else
        /// </summary>
        /// <remarks>
        ///  In this case any entity that references this item should be aware and 
        ///  take due actions (like de-referencing it)
        /// </remarks>
        bool IsNull { get; }

        IItemType<IItem> ItemType { get; }
    }
}
