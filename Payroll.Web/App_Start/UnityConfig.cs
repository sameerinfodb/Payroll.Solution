using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Payroll.Data.EntityFramework;
using Microsoft.AspNet.Identity;
using Payroll.Domain;
using Payroll.Web.Identity;
using Unity.Mvc5;
using System;

namespace Payroll.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor("DefaultConnection"));
            container.RegisterType<IUserStore<IdentityUser, Guid>, UserStore>(new TransientLifetimeManager());
            container.RegisterType<RoleStore>(new TransientLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
