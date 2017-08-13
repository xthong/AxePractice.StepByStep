using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using WebApplication;

namespace WebApi.Test
{
    public class TestBase : IDisposable
    {
        readonly HttpConfiguration httpConfiguration = new HttpConfiguration();
        HttpClient httpClient;
        HttpServer httpServer;

        protected void RegisterFakeInstance(Action<ContainerBuilder> action)
        {
            new BootStrapper () {BeforeBuildingContainerBuilder = action}.Init(httpConfiguration);
            httpServer = new HttpServer(httpConfiguration);
        }

        public  HttpClient CreateHttpClient()
        {
            httpClient = new HttpClient(httpServer)
            {
                BaseAddress = new Uri("http://baidu.com")
            };
            return httpClient;
        }

        public void Dispose()
        {
            httpClient?.Dispose();
            httpServer?.Dispose();
            httpConfiguration?.Dispose();
        }
    }
}