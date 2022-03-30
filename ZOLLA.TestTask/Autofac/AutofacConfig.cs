using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using ZOLLA.TestTask.Models;

namespace ZOLLA.TestTask.Autofac
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<ClientRepository>().As<IRepository<Client>>().WithParameter("context", new Context());
            builder.RegisterType<OrderRepository>().As<IRepository<Order>>().WithParameter("context", new Context());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}