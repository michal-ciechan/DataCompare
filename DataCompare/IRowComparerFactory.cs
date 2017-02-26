using System.Data;

namespace DataCompare
{
    public interface IRowComparerFactory
    {
        IRowComparer CreateValueComparer(IColumnMapper config, DataTable left, DataTable right);
        IRowComparer CreateKeyComparer(IKeyMapper colMap, DataTable left, DataTable right);
    }

    class RowComparerFactory : IRowComparerFactory
    {
        public IRowComparer CreateValueComparer(IColumnMapper colMap, DataTable left, DataTable right)
        {
            return new RowComparer(colMap.LeftColumns, colMap.RightColumns);
        }

        public IRowComparer CreateKeyComparer(IKeyMapper colMap, DataTable left, DataTable right)
        {
            return new RowComparer(colMap.LeftColumns, colMap.RightColumns);
        }
    }
}