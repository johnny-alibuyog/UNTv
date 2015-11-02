using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTv.WP81.Common.Extentions
{
    public static class CollectionExtention
    {
        public static bool IsNullOrEmpty(this IEnumerable items)
        {
            if (items == null)
                return true;

            var isEmpty = !items.GetEnumerator().MoveNext();
            if (isEmpty)
                return true;
    
            return false;
        }
    }
}
