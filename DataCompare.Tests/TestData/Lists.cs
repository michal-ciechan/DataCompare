using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompare.Tests.TestData
{
    public static class Lists
    {
        public static List<List<string>> Default => new List<List<string>>
        {
            new List<string> {"Key", "Value"},
            new List<string> {"1", "Test"},
        };

        public static List<List<string>> Add(this List<List<string>> list, int key, string value)
        {
            list.Add(new List<string> {key.ToString(), value});

            return list;
        } 
    }
}
