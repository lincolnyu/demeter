namespace Demeter.Abstract
{
    public interface IItemType<out T> where T : IItem
    {
        T Create();
    }
}
