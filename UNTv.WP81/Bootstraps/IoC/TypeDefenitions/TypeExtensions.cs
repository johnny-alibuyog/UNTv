using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTv.WP81.Bootstraps.IoC.TypeDefenitions
{
    public static class TypeExtensions
    {
        public static IEnumerable<BindingDefinition> GetBindingDefinitionOf(this IEnumerable<Type> types, Type openGenericType)
        {
            return types.Select(type => new InterfaceTypeDefinition(type))
                .Where(x => x.ImplementsOpenGenericTypeOf(openGenericType))
                .Select(x => new BindingDefinition(x, openGenericType));
        }

        public static bool IsOpenGeneric(this Type type, Type openGenericType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().IsAssignableFrom(openGenericType);
        }
    }
}
