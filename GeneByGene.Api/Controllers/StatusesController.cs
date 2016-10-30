using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GeneByGene.Api.Models;
using GeneByGene.Api.Repositories;

namespace GeneByGene.Api.Controllers
{
    public class StatusesController : ApiController
    {
        private readonly IStatusesRepository _statusesRepository;

        public StatusesController()
        {
            _statusesRepository = new StatusesRepository();
        }

        // GET api/statuses
        public IEnumerable<Status> Get()
        {
            return _statusesRepository.GetStatuses();
        }
    }
}
