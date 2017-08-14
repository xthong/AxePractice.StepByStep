﻿using System;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;

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
            BuildContainer(config);
            RegisterRoutes(config);
        }

        static void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("message", "message", new {controller = "message"});
            config.Routes.MapHttpRoute("get message by id", "message/{id}", new {controller = "message", action = "GetById"});
            config.Routes.MapHttpRoute("get status", "status", new {controller = "status"}, new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)}, new RequestDurationHandler(config));
        }

        void BuildContainer(HttpConfiguration config)
        {
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterType<MessageService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PerformanceLog>().As<IPerformanceLog>().InstancePerLifetimeScope();

            BeforeBuildingContainerBuilder(containerBuilder);
            config.Filters.Add(new RequestDurationFilter());
            config.DependencyResolver = new AutofacWebApiDependencyResolver(containerBuilder.Build());
        }
    }
}