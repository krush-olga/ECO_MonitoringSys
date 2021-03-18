using System;
using System.Collections.Generic;
using System.Collections;

namespace UserMap.Helpers
{
    /// <include file='Docs/Helpers/IEnumerableExtensionsDoc.xml' path='docs/members[@name="IEnumerable_extensions"]/IEnumerableExtensions/*'/>
    public static class IEnumerableExtensions
    {
        /// <include file='Docs/Helpers/IEnumerableExtensionsDoc.xml' path='docs/members[@name="IEnumerable_extensions"]/IndexOf/*'/>
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
        /// <include file='Docs/Helpers/IEnumerableExtensionsDoc.xml' path='docs/members[@name="IEnumerable_extensions"]/IndexOf/*'/>
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
