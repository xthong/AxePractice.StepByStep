using System.Threading.Tasks;
using Xunit;

namespace WebApi.Test
{
    public class TestClass : TestBase
    {
        [Fact]
        public async Task should_return_message()
        {
            var response = await Client.GetAsync("message");
            var actual = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hello", actual);
        }

        [Fact]
        public async Task should_return_message_by_id()
        {
            var response = await Client.GetAsync("message/1");
            var actual = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hi 1", actual);  
        }
    }
}
