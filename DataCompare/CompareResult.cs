using System;
using System.Collections.Generic;
using System.Data;

namespace DataCompare
{
    public class CompareResult
    {
        public struct Entry
        {
            public Entry(DataRow left, DataRow right)
            {
                Left = left;
                Right = right;
            }

            public DataRow Left;
            public DataRow Right;
        }

        /// <summary>
        /// Rows that matched by key and non-skipped values are the same.
        /// </summary>
        public List<Entry> Same { get; set; } = new List<Entry>();
        /// <summary>
        /// Rows that matched by key, but non-skipped values are different
        /// </summary>
        public List<Entry> Different { get; set; } = new List<Entry>();
        /// <summary>
        /// Rows that are only in the left DataTable as matched by Key
        /// </summary>
        public List<DataRow> LeftOnly { get; set; } = new List<DataRow>();
        /// <summary>
        /// Rows that are only in the right DataTable as matched by Key-
        /// </summary>
        public List<DataRow> RightOnly { get; set; } = new List<DataRow>();
    }
}