using System;
using System.Collections.Generic;
using System.Data;

namespace DataCompare
{
    public interface IRowComparer : IComparer<DataRow>
    {
    }

    class RowComparer : IRowComparer
    {
        private readonly List<IDataRowValueComparer> _comparers;

        public RowComparer(IReadOnlyList<DataColumn> leftColumns, IReadOnlyList<DataColumn> rightColumns)
        {
            _comparers = new List<IDataRowValueComparer>(leftColumns.Count);

            for (int i = 0; i < leftColumns.Count; i++)
            {
                var leftCol = leftColumns[i];
                var rightCol = rightColumns[i];

                var comparer = new DataRowValueComparer(leftCol, rightCol);

                _comparers.Add(comparer);
            }
        }

        public int Compare(DataRow left, DataRow right)
        {
            foreach (var comparer in _comparers)
            {
                var res = comparer.Compare(left, right);

                if(res == 0) continue;

                return res;
            }

            return 0;
        }
    }

    interface IDataRowValueComparer
    {
        int Compare(DataRow left, DataRow right);
    }

    class DataRowValueComparer : IDataRowValueComparer
    {
        private readonly DataColumn _leftCol;
        private readonly DataColumn _rightCol;

        public DataRowValueComparer(DataColumn leftCol, DataColumn rightCol)
        {
            _leftCol = leftCol;
            _rightCol = rightCol;
        }

        public int Compare(DataRow left, DataRow right)
        {
            var leftValue = left[_leftCol];
            var rightValue = right[_rightCol];

            if (leftValue == null && rightValue == null) 
                    return 0;

            if (leftValue == null)
                return -1;

            var leftComparable = leftValue as IComparable;
            
            if (leftComparable == null)
                throw new InvalidOperationException($"Cannot try to compare type {leftValue.GetType()} as it does not implement System.IComparable");

            return leftComparable.CompareTo(rightValue);
        }
    }
}