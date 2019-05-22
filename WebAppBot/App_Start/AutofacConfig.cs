using Autofac;
using Autofac.Integration.Mvc;
using BL;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAppBot.Controllers;
using WebAppBot.Dialogs;
using WebAppBot.Factory;

namespace WebAppBot.App_Start
{
    public class AutofacConfig
    {
        public static object data = new object();
        public static void RegisterAutofac()
        {

            var store = new InMemoryDataStore();

            Conversation.UpdateContainer(
                       builder =>
                       {
                           builder.Register(c => store)
                                 .Keyed<IBotDataStore<BotData>>(data)
                                 .AsSelf()
                                 .SingleInstance();

                           builder.Register(c => new CachingBotDataStore(store,
                                  CachingBotDataStoreConsistencyPolicy
                                  .ETagBasedConsistency))
                                         .As<IBotDataStore<BotData>>()
                                         .AsSelf()
                                         .InstancePerLifetimeScope();

                           builder.RegisterType<DialogFactory>()
                                   .Keyed<IDialogFactory>(FiberModule.Key_DoNotSerialize)
                                   .AsImplementedInterfaces()
                                   .InstancePerLifetimeScope();

                           builder.RegisterType<RootDialog>()
                                   .As<IDialog<object>>()
                                   .InstancePerDependency();

                          // builder.RegisterType<BusinessClass>().As<IBusinessClass>().InstancePerDependency();

                           builder.RegisterType<AppSettings>().As<IAppSettings>().InstancePerDependency();

                           builder.RegisterType<EchoDialog>().InstancePerDependency();

                           builder.RegisterType<BusinessClass>()
                                   .Keyed<IBusinessClass>(FiberModule.Key_DoNotSerialize)
                                   .AsImplementedInterfaces()
                                   .InstancePerDependency();

                           builder.RegisterType<HttpClient>()
                               .Keyed<HttpClient>(FiberModule.Key_DoNotSerialize)
                               .AsSelf()
                               .InstancePerDependency();

                           //register mvc controller
                           builder.RegisterControllers(typeof(LoginController).Assembly);
                       }
                    );

            //resolve mvc controller
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Conversation.Container));
        }
    }

}