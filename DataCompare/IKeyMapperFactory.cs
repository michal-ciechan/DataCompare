using System;
using System.Data;
using System.Linq;
using DataCompare.Common;

namespace DataCompare
{
    public interface IKeyMapperFactory
    {
        IKeyMapper Create(DataComparerConfig config, DataTable left, DataTable right);
    }

    class KeyMapperFactory : IKeyMapperFactory
    {
        public IKeyMapper Create(DataComparerConfig config, DataTable left, DataTable right)
        {
            var mapper = new KeyColumnMapper();

            var leftCols = left.Columns.Cast<DataColumn>().ToDictionary(x => x.ColumnName);
            var rightCols = right.Columns.Cast<DataColumn>().ToDictionary(x => x.ColumnName);

            foreach (var key in config.Keys)
            {
                if (!leftCols.TryGetValue(key, out var leftCol))
                    continue;

                if(!rightCols.TryGetValue(key, out var rightCol))
                    continue;

                mapper.Add(leftCol, rightCol);
            }

            if (mapper.LeftColumns.Count > 0)
                return mapper;

            // TODO: 5-Improve DataTables do not contain any specified keys warning message.
            Log.Warn($"Warning: Collections do not contain any key. Using first matching column.");

            foreach (DataColumn leftCol in left.Columns)
            {
                if (!rightCols.TryGetValue(leftCol.ColumnName, out var rightCol))
                    continue;

                mapper.Add(leftCol, rightCol);

                break;
            }

            if(mapper.LeftColumns.Count == 0)
                // TODO: 5-Improve no matching Key Columns message exception
                throw new InvalidOperationException("Could not find any mathcing Columns");

            return mapper;
        }
    }
}