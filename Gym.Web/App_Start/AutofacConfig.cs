using Autofac;
using Autofac.Integration.Mvc;
using Gym.Lib.Domain;
using Gym.Lib.Services;
using Gym.Lib.Services.Customers;
using Gym.Lib.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Gym.Web.App_Start
{
    public class AutofacConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<gymEntities>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<SubscriptionService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ReservationService>().AsSelf().InstancePerLifetimeScope();

            


        }
    }
}