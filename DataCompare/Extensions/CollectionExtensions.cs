using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompare.Extensions
{
    public static class CollectionExtensions
    {
        public static ICollection<T> With<T>(this ICollection<T> collection, Action<ICollection<T>> action)
        {
            action(collection);

            return collection;
        }
    }
}
