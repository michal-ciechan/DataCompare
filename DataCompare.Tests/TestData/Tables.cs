using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataCompare.Tests.TestData
{
    public static class Tables
    {
        public static DataTable Default
        {
            get
            {
                var table = new DataTable();

                table.Columns.Add("Key", typeof(int));
                table.Columns.Add("Value", typeof(string));

                table.Rows.Add(0, "First");
                table.Rows.Add(1, "Second");

                return table;
            }
        }

        public static string GetValue(this DataRow row)
        {
            return row["Value"] as string;
        }

        public static int Key(this DataRow row)
        {
            return (int) row["Key"];
        }

        public static DataRow Last(this DataRowCollection rows)
        {
            return rows[rows.Count - 1];
        }

        public static IEnumerable<object> Items(this IEnumerable<DataRow> rows)
        {
            return rows.Select(x => x.ItemArray);
        }
        public static IEnumerable<IEnumerable<object>> Values(this IEnumerable<CompareResult.Entry> results)
        {
            return results.Select(x => new[] {x.Left.GetValue(), x.Right.GetValue()});
        }

        public static DataTable Add(this DataTable table, int key, string value, params string[] values)
        {
            if (values == null)
            {
                table.Rows.Add(key, value);
            }
            else
            {
                var vals = new object[values.Length + 2];

                vals[0] = key;
                vals[1] = value;

                Array.Copy(values, 0, vals, 2, values.Length);

                table.Rows.Add(vals);
            }

            return table;
        }

        public static DataTable AddColumn<T>(this DataTable table, string name, T value = default(T))
        {
            table.Columns.Add(name, typeof(T));

            if (EqualityComparer<T>.Default.Equals(value, default(T)))
                return table;

            foreach (DataRow row in table.Rows)
            {
                row[name] = value;
            }

            return table;
        } 
    }
}
