using Demeter.Abstract;

namespace Demeter.Entities
{
    public class BaseType<T> : IItemType<T> where T : BaseItem, new()
    {
        public virtual T Create() => Create<T>();

        public virtual T2 Create<T2>() where T2 : BaseItem, new()
        {
            var t2 = new T2
            {
                ItemType = this
            };
            return t2;
        }
    }
}
