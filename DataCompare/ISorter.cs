using System.Collections.Generic;

namespace DataCompare
{
    public interface ISorter<T>
    {
        IReadOnlyList<T> Sort(IReadOnlyList<T> list);
    }
}