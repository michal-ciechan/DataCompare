using System;
using System.Collections.Generic;

namespace DataCompare
{
    public class DataComparer
    {
        private readonly ISorter<IReadOnlyList<string>> _sorter;
        private DataComparerConfig _config;

        public DataComparer(ISorter<IReadOnlyList<string>> sorter, DataComparerConfig config)
        {
            _sorter = sorter;
            _config = config;
        }

        public CompareResult Compare(IReadOnlyList<IReadOnlyList<string>> left,
            IReadOnlyList<IReadOnlyList<string>> right)
        {
            var leftSorted = _sorter.Sort(left);
            var rightSorted = _sorter.Sort(right);

            return CompareSorted(leftSorted, rightSorted);
        }

        private CompareResult CompareSorted(IReadOnlyList<IReadOnlyList<string>> left,
            IReadOnlyList<IReadOnlyList<string>> right)
        {
            throw new NotImplementedException();
        }
    }
}