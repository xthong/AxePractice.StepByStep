using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication.Controllers
{
    public class MessageController : ApiController
    {
        readonly MessageService messageService;

        public MessageController(MessageService messageService)
        {
            this.messageService = messageService;
        }

        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(messageService.SayHello());

            return response;
        }

        public HttpResponseMessage GetById(long id)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent($"Hi {id}");
            return response;
        }
    }
}