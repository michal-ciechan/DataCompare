using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace DataCompare
{
    public class RowCollection
    {
        public RowCollection(DataComparerConfig config,
            IReadOnlyList<IReadOnlyList<string>> source)
        {
            Config = config ?? DataComparerConfig.Default;
            Source = source;
            KeyColumnIndexes = new List<int>();
            SkippedColumnIndexes = new List<int>();
            DataColumnIndexes = new List<int>();

            SplitHeaderAndData(source);

            FindColumnIndexes();
        }

        private void FindColumnIndexes()
        {
            for (var i = 0; i < Headers.Count; i++)
            {
                var value = Headers[i];

                if (Config.Keys.Contains(value))
                    KeyColumnIndexes.Add(i);

                if(Config.Skip.Contains(value))
                    SkippedColumnIndexes.Add(i);

                DataColumnIndexes.Add(i);
            }
        }


        private void SplitHeaderAndData(IReadOnlyList<IReadOnlyList<string>> source)
        {
            Headers = Source.First();
            Data = Source.Skip(1).ToList();
        }

        public List<int> KeyColumnIndexes { get; set; }
        public List<int> SkippedColumnIndexes { get; set; }
        public List<int> DataColumnIndexes { get; set; }
        public IReadOnlyList<IReadOnlyList<string>> Data { get; set; }
        public IReadOnlyList<string> Headers { get; set; }
        public DataComparerConfig Config { get; set; }
        public IReadOnlyList<IReadOnlyList<string>> Source { get; private set; }


        public static RowCollection Parse(IReadOnlyList<IReadOnlyList<string>> rows
            , DataComparerConfig config = null)
        {
            var res = new RowCollection(config, rows);

            return res;
        }
    }
}