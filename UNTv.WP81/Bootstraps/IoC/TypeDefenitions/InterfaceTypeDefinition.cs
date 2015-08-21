using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTv.WP81.Bootstraps.IoC.TypeDefenitions
{
    public class InterfaceTypeDefinition
    {
        /// <summary>
        /// The concrete implementation.
        /// </summary>
        public Type Implementation { get; private set; }

        /// <summary>
        /// The interfaces implemented by the implementation.
        /// </summary>
        public IEnumerable<Type> Interfaces { get; private set; }

        public InterfaceTypeDefinition(Type type)
        {
            Implementation = type;
            Interfaces = type.GetInterfaces();
        }

        /// <summary>
        /// Returns a value indicating whether the implementation
        /// implements the specified open generic type.
        /// </summary>
        public bool ImplementsOpenGenericTypeOf(Type openGenericType)
        {
            return Interfaces.Any(i => i.IsOpenGeneric(openGenericType));
        }

        /// <summary>
        /// Returns the service type for the concrete implementation.
        /// </summary>
        public Type GetService(Type openGenericType)
        {
            return Interfaces
                .First(x => x.IsOpenGeneric(openGenericType))
                .GetGenericArguments()
                .Select(x => openGenericType.MakeGenericType(x))
                .First();
        }
    }
}
