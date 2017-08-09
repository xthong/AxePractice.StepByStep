using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication;
using Xunit;

namespace WebApi.Test
{
    public class TestClass
    {
        [Fact]
        public async Task should_return_message()
        {
            var httpConfiguration = new HttpConfiguration();
            BootStrapper.Init(httpConfiguration);
            
            using(var httpServer = new HttpServer(httpConfiguration))
            {
                var client = new HttpClient(httpServer)
                {
                    BaseAddress = new Uri("http://baidu.com")
                };

                var response = await client.GetAsync("message");
                var actual = await response.Content.ReadAsStringAsync();
                Assert.Equal("Hello", actual);    
            }
        }
    }
}
