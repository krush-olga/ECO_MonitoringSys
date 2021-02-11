using System;
using System.Collections.Generic;
using System.Collections;

namespace UserMap.Helpers
{
    public static class IEnumerableExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicat)
        {
            int index = -1;
            foreach (var item in collection)
            {
                index++;
                if (predicat(item))
                {
                    return index;
                }
            }

            return index;
        }

        public static int IndexOf<T>(this IEnumerable collection, Func<T, bool> predicat)
        {
            int index = -1;
            foreach (T item in collection)
            {
                index++;
                if (predicat(item))
                {
                    return index;
                }
            }

            return index;
        }
    }
}
