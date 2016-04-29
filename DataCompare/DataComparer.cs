using System.Collections.Generic;

namespace DataCompare
{
    public class DataComparer
    {
        public CompareResult Compare(IReadOnlyList<IList<string>> array1, IReadOnlyList<IList<string>> array2)
        {
            var res = new CompareResult
            {
                Same = true
            };

            return res;
        }
    }

    public class CompareResult
    {
        public bool Same { get; set; }
        public IReadOnlyList<IReadOnlyList<string>> Additional { get; set; }
        public IReadOnlyList<IReadOnlyList<string>> Missing { get; set; }
    }

}
