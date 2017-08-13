using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using WebApplication.Controllers;

namespace WebApplication
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var httpConfiguration = GlobalConfiguration.Configuration;
            var bootStrapper = new BootStrapper();
            bootStrapper.Init(httpConfiguration);
        }
    }

    public class BootStrapper
    {
        readonly ContainerBuilder containerBuilder;

        public BootStrapper()
        {
            containerBuilder = new ContainerBuilder();
        }

        public Action<ContainerBuilder> BeforeBuildingContainerBuilder { get; set; }

        public void Init(HttpConfiguration config)
        {
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterType<MessageService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PerformanceLog>().As<IPerformanceLog>().InstancePerLifetimeScope();

            BeforeBuildingContainerBuilder(containerBuilder);

            config.Filters.Add(new RequestDurationFilter());
            config.DependencyResolver = new AutofacWebApiDependencyResolver(containerBuilder.Build());
            config.Routes.MapHttpRoute("message","message", new { controller = "message"});
            config.Routes.MapHttpRoute("get message by id","message/{id}", new { controller = "message", action = "GetById" });
        }
    }
}