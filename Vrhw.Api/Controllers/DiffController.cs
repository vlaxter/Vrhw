using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vrhw.Shared.Interfaces;
using Vrhw.Shared.Models;

namespace Vrhw.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("v1/diff")]
    public class DiffController : ApiController
    {
        private readonly IDiffService _diffService;

        public DiffController(IDiffService diffService)
        {
            _diffService = diffService;
        }

        /// <summary>
        /// Insert a Base64 string into the Left field.
        /// </summary>
        /// <remarks>
        /// Insert a Base64 string into the Left field of the given Id, If the id doesn't exists the entry is created.
        /// </remarks>
        /// <param name="id">ID of the Diff.</param>
        /// <param name="request">Json request containing a base64 string</param>
        /// <returns>Returns a Http response</returns>
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

        /// <summary>
        /// Insert a Base64 string into the Right field.
        /// </summary>
        /// <remarks>
        /// Insert a Base64 string into the Right field of the given Id, If the id doesn't exists the entry is created.
        /// </remarks>
        /// <param name="id">ID of the Diff.</param>
        /// <param name="request">Json request containing a base64 string</param>
        /// <returns>Returns a Http response</returns>
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

        /// <summary>
        /// Evaluate if the Left and Right fields are different.
        /// </summary>
        /// <remarks>
        /// Evaluate if the Left and Right fields are equal, diferent in size or if Left and Right are the same size, where are the differences.
        /// </remarks>
        /// <param name="id">ID of the Diff.</param>
        /// <returns>Returns the different result type and the differences if apply.</returns>
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