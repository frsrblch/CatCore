using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Yield<T>(this T item)
        {
            if (item != null)
            {
                yield return item;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}
