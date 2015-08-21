using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTv.WP81.Bootstraps.IoC.TypeDefenitions
{
    public class BindingDefinition
    {
        public Type Service { get; private set; }

        public Type Implementation { get; private set; }

        public BindingDefinition(InterfaceTypeDefinition definition, Type openGenericType)
        {
            Implementation = definition.Implementation;
            Service = definition.GetService(openGenericType);
        }
    }
}
