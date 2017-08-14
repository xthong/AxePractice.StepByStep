using System.Threading.Tasks;
using Autofac;
using Moq;
using WebApplication;
using Xunit;

namespace WebApi.Test
{
    public class TestClass : TestBase
    {
        [Fact]
        public async Task should_return_message()
        {
            var mockLog = new Mock<IPerformanceLog>();
            RegisterFakeInstance(c => c.RegisterInstance(mockLog.Object).As<IPerformanceLog>());

            var client = CreateHttpClient();
            var response = await client.GetAsync("message");
            var actual = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hello", actual);
            mockLog.Verify(l => l.Log(It.Is<string>(e => e.Contains("Request started"))), Times.Once);
            mockLog.Verify(l => l.Log(It.Is<string>(e => e.Contains("Request total time is"))), Times.Once);
        }

        [Fact]
        public async Task should_return_message_by_id()
        {
            var mockLog = new Mock<IPerformanceLog>();
            RegisterFakeInstance(c => c.RegisterInstance(mockLog.Object).As<IPerformanceLog>());
            var client = CreateHttpClient();
            var response = await client.GetAsync("message/1");
            var actual = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hi 1", actual);  
            mockLog.Verify(l => l.Log(It.Is<string>(e => e.Contains("Request started"))), Times.Once);
            mockLog.Verify(l => l.Log(It.Is<string>(e => e.Contains("Request total time is"))), Times.Once);
        }

        [Fact]
        public async Task should_return_status()
        {
            var mockLog = new Mock<IPerformanceLog>();
            RegisterFakeInstance(c => c.RegisterInstance(mockLog.Object).As<IPerformanceLog>());

            var client = CreateHttpClient();
            var response = await client.GetAsync("status");

            var actual = await response.Content.ReadAsStringAsync();
            Assert.Equal("Get all status", actual);
            mockLog.Verify(l => l.Log(It.Is<string>(e => e.Contains("starting"))), Times.Once);
            mockLog.Verify(l => l.Log(It.Is<string>(e => e.Contains("request total time is"))), Times.Once);
        }
    }
}
