using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ReactiveUI;
using UNTv.WP81.Features;
using UNTv.WP81.Features.Controls.ListItemControls;
using UNTv.WP81.Features.News;

namespace UNTv.WP81.Bootstraps.IoC.Modules
{
    public class FeatureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /// References
            /// autofact_reactiveui:    http://www.zimarev.com/2014/04/autofac-and-reactiveui.html
            /// modules:                http://autofac.readthedocs.org/en/latest/configuration/modules.html
            /// open_generic_modules:   http://stackoverflow.com/questions/16757945/how-to-register-many-for-open-generic-in-autofac

            builder.RegisterType<RoutingState>()
                .AsSelf().SingleInstance();

            builder.RegisterType<ShellViewModel>()
                .As<IScreen>().SingleInstance();

            //builder.RegisterType<ShellView>().AsSelf();

            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .AsClosedTypesOf(typeof(IViewFor<>))
                .AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(this.ThisAssembly)
            //    ///.Where(x => x is ReactiveBase)
            //    .AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(this.ThisAssembly)
            //    .AsSelf()
            //    //.Where(x => x is ReactiveRoutableBase)
            //    .AsImplementedInterfaces();


            //builder.RegisterType<RoutingState>()
            //    .As<RoutingState>().SingleInstance();

            //builder.RegisterType<ShellViewModel>()
            //    .As<IScreen>().SingleInstance();

            //builder.RegisterType<ShellView>().AsSelf();

            builder.RegisterType<ShellView>().As<IViewFor<ShellViewModel>>();
            builder.RegisterType<HubPageView>().As<IViewFor<HubPageViewModel>>();
            builder.RegisterType<NewsPageView>().As<IViewFor<NewsPageViewModel>>();
            builder.RegisterType<NewsSectionView>().As<IViewFor<NewsSectionViewModel>>();
            builder.RegisterType<DoubleLineView>().As<IViewFor<DoubleLineViewModel>>();

            builder.RegisterType<ShellViewModel>().AsSelf();
            builder.RegisterType<HubPageViewModel>().AsSelf();
            builder.RegisterType<NewsPageViewModel>().AsSelf();
            builder.RegisterType<NewsSectionViewModel>().AsSelf();
            builder.RegisterType<DoubleLineViewModel>().AsSelf();
        }
    }
}
