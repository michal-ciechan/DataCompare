using System.Collections.Generic;

namespace DataCompare
{
    /// <summary>
    /// IMPORTANT! If you update this, extend this, make sure you
    /// 1. Create tests in Readme/Defaults.cs
    /// 2. Update the README.ms [#Default] section
    /// </summary>
    public class DataComparerConfig
    {
        public static DataComparerConfig CreateDefault()
        {
            return new DataComparerConfig
            {
                Keys = new HashSet<string>
                {
                    "Key"
                },
                Skip = new HashSet<string>
                {
                    "ID", "Skip"
                }
            };
        }

        public HashSet<string> Keys { get; set; }
        public HashSet<string> Skip { get; set; }
    }
}