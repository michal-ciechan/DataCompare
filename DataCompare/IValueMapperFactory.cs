using System;
using System.Data;
using System.Linq;
using DataCompare.Common;

namespace DataCompare
{
    public interface IValueMapperFactory
    {
        IColumnMapper Create(DataComparerConfig config, IKeyMapper keyMap, DataTable left, DataTable right);
    }

    internal class ValueMapperFactory : IValueMapperFactory
    {
        public IColumnMapper Create(DataComparerConfig config, IKeyMapper keyMap, DataTable left, DataTable right)
        {
            var mapper = new ColumnMapper();

            var leftCols = left.Columns.Cast<DataColumn>().ToDictionary(x => x.ColumnName);
            var rightCols = right.Columns.Cast<DataColumn>().ToDictionary(x => x.ColumnName);


            foreach (DataColumn leftCol in left.Columns)
            {
                var name = leftCol.ColumnName;

                if(config.Keys.Contains(name))
                    continue;

                if(config.Skip.Contains(name))
                    continue;

                if (!rightCols.TryGetValue(name, out var rightCol))
                    continue;

                mapper.Add(leftCol, rightCol);

                break;
            }
            
            if (mapper.LeftColumns.Count == 0)
                // TODO: 5-Improve no matching Value Columns message exception
                throw new InvalidOperationException("Could not find any mathcing Value Columns");

            return mapper;
        }
    }
}