using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace UNTv.WP81.Bootstraps.IoC.TypeDefenitions
{
    class SampleImplementation
    {
        /// <summary>
        /// http://stackoverflow.com/questions/2216127/ninject-with-generic-interface
        /// </summary>
        public void Load()
        {
            var definitions = Assembly.GetExecutingAssembly().GetTypes()
                .GetBindingDefinitionOf(typeof(IViewFor<>));

            foreach (var definition in definitions)
            {
                Bind(definition.Service).To(definition.Implementation);

                builder
                    .Register(definition.Service)
                    .As(definition.Implementation)
                    .InstancePerDependency();
            }
        }
    }
}
