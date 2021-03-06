namespace Veruthian.Library.Types
{
    public interface Has<T>
    {
        void Get(out T value);
    }

    public interface HasSettable<T> : Has<T>
    {
        void Set(T value);
    }

}