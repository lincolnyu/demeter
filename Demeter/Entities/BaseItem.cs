using Demeter.Abstract;

namespace Demeter.Entities
{
    public abstract class BaseItem : IItem
    {
        public BaseItem()
        {
        }

        public BaseItem(IItemType<IItem> type, string name = null, string description = null)
        {
            ItemType = type;
            Name = name;
            Description = description;
        }

        public BaseItem(string name, string description = null)
        {
            Name = name;
            Description = description;
        }

        #region IItem members

        public abstract bool IsNull { get; }

        public abstract IItem Clone();

        public virtual IItemType<IItem> ItemType
        {
            get; set;
        }

        #endregion


        public string Name { get; set; }

        public string Description { get; set; }
    }
}
