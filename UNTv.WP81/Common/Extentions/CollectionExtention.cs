using System.Collections;
using System.Diagnostics;

namespace UNTv.WP81.Common.Extentions
{
    public static class CollectionExtention
    {
        public static bool IsNull(this IEnumerable items)
        {
            return items == null;
        }

        public static bool IsNullOrEmpty(this IEnumerable items)
        {
            Debug.WriteLine(items);

            if (items == null)
                return true;

            var isEmpty = !items.GetEnumerator().MoveNext();
            if (isEmpty)
                return true;
    
            return false;
        }
    }
}
