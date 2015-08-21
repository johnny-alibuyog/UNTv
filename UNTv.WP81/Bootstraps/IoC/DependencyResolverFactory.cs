using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Splat;
using UNTv.WP81.Bootstraps.IoC.Modules;

namespace UNTv.WP81.Bootstraps.IoC
{
    public class DependencyResolverFactory
    {
        public static IMutableDependencyResolver Create()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<FeatureModule>();
            builder.RegisterModule<MediatRModule>();

            var container = builder.Build();
            return new AutofacDependencyResolver(container);
        }
    }
}
