using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GeneByGene.Api.Repositories;

namespace GeneByGene.Api.Controllers
{
    public class StatusesController : ApiController
    {
        private readonly IStatusesRepository _statusesRepository;

        public StatusesController(IStatusesRepository statusesRepository)
        {
            _statusesRepository = statusesRepository;
        }

        // GET api/statuses
        public HttpResponseMessage Get()
        {
            try
            {
                var statuses = _statusesRepository.GetStatuses();
                return Request.CreateResponse(HttpStatusCode.OK, statuses);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
