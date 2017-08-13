using System;
using System.IO;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication
{
    public class RequestDurationFilter : ActionFilterAttribute
    {
        readonly string requestStartTime= "requestStartTime";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            actionContext.Request.Properties[requestStartTime] = DateTime.UtcNow;
            var performanceLog = (IPerformanceLog) actionContext.Request.GetDependencyScope().GetService(typeof(IPerformanceLog));
            performanceLog.Log("Request started");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var performanceLog = (IPerformanceLog) actionExecutedContext.Request.GetDependencyScope().GetService(typeof(IPerformanceLog));
            var requestEndTime = DateTime.UtcNow;
            object startTime;
            actionExecutedContext.Request.Properties.TryGetValue(requestStartTime, out startTime);
            if (startTime != null)
            {
                var actualTime = requestEndTime.Subtract((DateTime)startTime);
                performanceLog.Log($"Request total time is {actualTime.Milliseconds} ms");
            }
        }
    }

    public class PerformanceLog : IPerformanceLog
    {
        public virtual void Log(string message)
        {
            using(var writer = new StreamWriter("C:\\Users\\Administrator\\Desktop\\Log\\log.txt", true))
            {
                writer.WriteLine(message);
            }
        }
    }

    public interface IPerformanceLog
    {
        void Log(string message);
    }
}