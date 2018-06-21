using System.Collections.Generic;

namespace Veruthian.Dotnet.Library.Data.Collections
{
    public interface IExpandableIndex<T> : IMutableIndex<T>, IExpandableContainer<T>, IExpandableLookup<int, T>
    {
        void AddRange(IEnumerable<T> values);

        void InsertRange(int index, IEnumerable<T> values);

        void RemoveRange(int start, int count);
    }
}