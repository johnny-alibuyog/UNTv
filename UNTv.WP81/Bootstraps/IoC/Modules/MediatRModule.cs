using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.Variance;
using MediatR;

namespace UNTv.WP81.Bootstraps.IoC.Modules
{
    public class MediatRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(this.ThisAssembly)
            //    .AsImplementedInterfaces();

            //builder.RegisterInstance(Console.Out).As<TextWriter>();

            builder.Register<SingleInstanceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return type => componentContext.Resolve(type);
            });

            builder.Register<MultiInstanceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return type => (IEnumerable<object>)componentContext.Resolve(typeof(IEnumerable<>).MakeGenericType(type));
            });

        }
    }
}
