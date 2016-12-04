using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vrhw.Shared.Interfaces;
using Vrhw.Shared.Models;

namespace Vrhw.Api.Controllers
{
    [RoutePrefix("v1/diff")]
    public class DiffController : ApiController
    {
        private readonly IDiffService _diffService;

        public DiffController(IDiffService diffService)
        {
            _diffService = diffService;
        }

        [Route("{id:int}/left")]
        [HttpPut]
        public HttpResponseMessage Left(int id, [FromBody]RequestModel request)
        {
            var status = HttpStatusCode.BadRequest;
            if (request != null && !string.IsNullOrWhiteSpace(request.Data))
            {
                status = _diffService.Left(id, request.Data) ? HttpStatusCode.Created : HttpStatusCode.BadRequest;
            }

            return Request.CreateResponse(status);
        }

        [Route("{id:int}/right")]
        [HttpPut]
        public HttpResponseMessage Right(int id, [FromBody]RequestModel request)
        {
            var status = HttpStatusCode.BadRequest;
            if (request != null && !string.IsNullOrWhiteSpace(request.Data))
            {
                status = _diffService.Right(id, request.Data) ? HttpStatusCode.Created : HttpStatusCode.BadRequest;
            }

            return Request.CreateResponse(status);
        }

        [Route("{id:int}")]
        [HttpGet]
        public HttpResponseMessage Diff(int id)
        {
            var diff = _diffService.GetDiff(id);
            if (diff == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK, diff);
        }
    }
}