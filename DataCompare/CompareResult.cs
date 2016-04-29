using System.Collections.Generic;

namespace DataCompare
{
    public class CompareResult
    {
        public bool Same { get; set; }
        public IReadOnlyList<IReadOnlyList<string>> LeftOnly { get; set; }
        public IReadOnlyList<IReadOnlyList<string>> RightOnly { get; set; }
    }
}