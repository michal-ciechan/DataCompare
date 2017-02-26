using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DataCompare.Enums;

namespace DataCompare
{
    //class DataTable
    //{
    //    public IList<Column> Columns { get; set; }
    //    public IList<Data> Data { get; set; }

    //}

    //class Data
    //{
    //}

    //class Column
    //{
    //}

    public class DataComparer
    {
        public DataComparerConfig Config { get; }
        private readonly IKeyMapperFactory _keyMapperFactory;
        private readonly IRowComparerFactory _rowComparerFactory;
        private readonly ISorter _sorter;
        private readonly IValueMapperFactory _valueMapperFactory;
        private IRowComparer _keyComparer;
        private IKeyMapper _keyMapper;
        private readonly Stopwatch _stopwatch;
        private IRowComparer _valueComparer;
        private IColumnMapper _valueMapper;

        public DataComparer(DataComparerConfig config, ISorter sorter, IRowComparerFactory rowComparerFactory,
            IKeyMapperFactory keyMapperFactory, IValueMapperFactory valueMapperFactory)
        {
            _sorter = sorter;
            _rowComparerFactory = rowComparerFactory;
            _keyMapperFactory = keyMapperFactory;
            _valueMapperFactory = valueMapperFactory;
            Config = config;
            _stopwatch = new Stopwatch();
        }

        public bool HasHeaders { get; set; }

        public TimeSpan TimeTakenForSorting { get; set; }

        public TimeSpan TimeTakenForComparing { get; set; }

        public CompareResult Compare(DataTable left, DataTable right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            _keyMapper = _keyMapperFactory.Create(Config, left, right);
            _valueMapper = _valueMapperFactory.Create(Config, _keyMapper, left, right);
            
            _stopwatch.Restart();

            var sorted = Config.IsSorted;

            var leftSorted = sorted != DataSource.Left && sorted != DataSource.Both
                ? _sorter.Sort(_keyMapper, left, DataSource.Left)
                : left;

            var rightSorted = sorted != DataSource.Right && sorted != DataSource.Both
                ? _sorter.Sort(_keyMapper, right, DataSource.Right)
                : right;

            _stopwatch.Stop();

            TimeTakenForSorting = _stopwatch.Elapsed;


            return CompareSorted(leftSorted, rightSorted);
        }

        private CompareResult CompareSorted(DataTable left, DataTable right)
        {
            _stopwatch.Restart();

            var result = new CompareResult();

            _keyMapper = _keyMapperFactory.Create(Config, left, right);
            _valueMapper = _valueMapperFactory.Create(Config, _keyMapper, left, right);

            _valueComparer = _rowComparerFactory.CreateValueComparer(_valueMapper, left, right);
            _keyComparer = _rowComparerFactory.CreateKeyComparer(_keyMapper, left, right);

            var leftIx = 0;
            var rightIx = 0;

            // Go Through All Left Comparing to Right
            while (leftIx < left.Rows.Count && rightIx < right.Rows.Count)
            {
                var leftRow = left.Rows[leftIx];
                var rightRow = right.Rows[rightIx];

                var keyResult = _keyComparer.Compare(leftRow, rightRow);

                if (keyResult == 0)
                {
                    var valueResult = _valueComparer.Compare(leftRow, rightRow);

                    if (valueResult == 0)
                        result.Same.Add(new CompareResult.Entry(leftRow, rightRow));
                    else
                        result.Different.Add(new CompareResult.Entry(leftRow, rightRow));

                    leftIx++;
                    rightIx++;
                }
                else if (keyResult < 0)
                {
                    result.LeftOnly.Add(leftRow);

                    leftIx++;
                }
                else
                {
                    result.RightOnly.Add(rightRow);

                    rightIx++;
                }
            }

            while (leftIx < left.Rows.Count)
            {
                var leftRow = left.Rows[leftIx];

                result.LeftOnly.Add(leftRow);

                leftIx++;
            }

            while (rightIx < right.Rows.Count)
            {
                var rightRow = right.Rows[rightIx];

                result.RightOnly.Add(rightRow);

                rightIx++;
            }

            _stopwatch.Stop();
            TimeTakenForComparing = _stopwatch.Elapsed;

            return result;
        }
    }

    public interface IKeyMapper : IColumnMapper
    {
    }

    public interface IColumnMapper
    {
        IReadOnlyList<DataColumn> LeftColumns { get; }
        IReadOnlyList<DataColumn> RightColumns { get; }
    }

    internal class KeyColumnMapper : ColumnMapper, IKeyMapper
    {
    }

    internal class ColumnMapper : IColumnMapper
    {
        private readonly List<DataColumn> _leftColumns;
        private readonly List<DataColumn> _rightColumns;

        public ColumnMapper()
        {
            _leftColumns = new List<DataColumn>();
            _rightColumns = new List<DataColumn>();
        }

        public IReadOnlyList<DataColumn> LeftColumns => _leftColumns;
        public IReadOnlyList<DataColumn> RightColumns => _rightColumns;

        public void Add(DataColumn left, DataColumn right)
        {
            _leftColumns.Add(left);
            _rightColumns.Add(right);
        }
    }
}