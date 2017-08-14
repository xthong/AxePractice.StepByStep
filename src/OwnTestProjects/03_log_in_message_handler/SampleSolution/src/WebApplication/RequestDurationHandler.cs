using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace WebApplication
{
    public class RequestDurationHandler : DelegatingHandler
    {
        public RequestDurationHandler(HttpConfiguration config)
        {
            InnerHandler = new HttpControllerDispatcher(config);
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logger = (IPerformanceLog)request.GetDependencyScope().GetService(typeof(IPerformanceLog));
            logger.Log($"{request.RequestUri} starting");

            var startTime = DateTime.UtcNow;

            return base.SendAsync(request, cancellationToken).ContinueWith(t =>
            {
                var actualTime = DateTime.UtcNow.Subtract(startTime);

                logger.Log($"{request.RequestUri} request total time is {actualTime.Milliseconds} ms");
                return t.Result;
            }, cancellationToken);

        }
    
    }
}