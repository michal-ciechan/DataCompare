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

        public static List<List<string>> Add(this List<List<string>> list, int key, string value, params string[] values)
        {
            var item = new List<string> {key.ToString(), value};
           
            if(values != null) item.AddRange(values);

            list.Add(item);

            return list;
        } 

        public static List<List<string>> AddColumn(this List<List<string>> list, string header, string value = null)
        {
            value = value ?? new Random().Next().ToString();

            list.First().Add(header); // Header

            foreach (var x in list.Skip(1))
            {
                x.Add(value);
            }

            return list;
        } 
    }
}
