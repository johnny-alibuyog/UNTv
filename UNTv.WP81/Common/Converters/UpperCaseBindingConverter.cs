using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTv.WP81.Common.Converters
{
    public class UpperCaseBindingConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            return (toType == typeof(string) ? 2 : 0);
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            result = ((string)from).ToUpper();
            return true;
        }
    }
}
