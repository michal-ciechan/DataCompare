using System;
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
        static DataComparerConfig()
        {
            Default = CreateDefault();
        }

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
                },
                HasHeaders = true,
            };
        }

        public bool HasHeaders
        {
            get { return true; }
            set { if(value == false) throw new NotImplementedException(); }
        }
        public HashSet<string> Keys { get; set; }
        public HashSet<string> Skip { get; set; }
        
        public static DataComparerConfig Default { get; set; }
    }
}