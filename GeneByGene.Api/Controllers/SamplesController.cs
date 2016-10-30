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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SamplesController : ApiController
    {
        private readonly ISamplesRepository _samplesRepository;

        public SamplesController()
        {
            _samplesRepository = new SamplesRepository();
        }

        // GET api/samples
        public IEnumerable<Sample> Get()
        {
            return _samplesRepository.GetSamples();
        }

        // GET api/samples/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/samples
        public void Post([FromBody]string value)
        {
        }

        // PUT api/samples/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/samples/5
        public void Delete(int id)
        {
        }
    }
}
