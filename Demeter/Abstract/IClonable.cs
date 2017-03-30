namespace Demeter.Abstract
{
    public interface IClonable<out T>
    {
        T Clone();
    }
}
