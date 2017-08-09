using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace WebApplication
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var httpConfiguration = GlobalConfiguration.Configuration;
            BootStrapper.Init(httpConfiguration);
        }
    }

    public class BootStrapper
    {
        public static void Init(HttpConfiguration httpConfiguration)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterType<MessageService>();

            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(containerBuilder.Build());
            httpConfiguration.Routes.MapHttpRoute("message","message", new { controller = "message"});
        }
    }
}