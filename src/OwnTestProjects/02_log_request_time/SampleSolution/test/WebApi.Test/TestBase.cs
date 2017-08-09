using System;
using System.Net.Http;
using System.Web.Http;
using WebApplication;

namespace WebApi.Test
{
    public class TestBase : IDisposable
    {
        readonly HttpConfiguration httpConfiguration;
        readonly HttpServer httpServer;
        protected HttpClient Client {get;}

        public TestBase()
        {
            httpConfiguration = new HttpConfiguration();
            BootStrapper.Init(httpConfiguration);
            httpServer = new HttpServer(httpConfiguration);
            Client = CreateHttpClient(httpServer);
        }

        static HttpClient CreateHttpClient(HttpServer httpServer)
        {
            var client = new HttpClient(httpServer)
            {
                BaseAddress = new Uri("http://baidu.com")
            };
            return client;
        }

        public void Dispose()
        {
            Client?.Dispose();
            httpServer?.Dispose();
        }
    }
}