using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using DataCompare.Enums;

namespace DataCompare
{
    public interface ISorter
    {
        DataTable Sort(IKeyMapper keyMapper, DataTable data, DataSource source);
    }

    class Sorter : ISorter
    {
        public DataTable Sort(IKeyMapper keyMapper, DataTable data, DataSource source)
        {
            var cols = source == DataSource.Left 
                ? keyMapper.LeftColumns 
                : keyMapper.RightColumns;

            var view = data.DefaultView;

            var sb = new StringBuilder();

            bool first = true;

            foreach (var col in cols)
            {
                if (!first)
                    sb.Append(", ");

                sb.Append(col.ColumnName);
                sb.Append(" ASC");

                first = !first;
            }

            view.Sort = sb.ToString();

            var res = view.ToTable(source.ToString());

            return res;
        }
    }
}