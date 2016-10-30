using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using GeneByGene.Api.Dtos;
using GeneByGene.Api.Models;
using GeneByGene.Api.Repositories;

namespace GeneByGene.Api.Controllers
{
    public class SamplesController : ApiController
    {
        private readonly ISamplesRepository _samplesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IStatusesRepository _statusesRepository;

        public SamplesController()
        {
            _samplesRepository = new SamplesRepository();
            _usersRepository = new UsersRepository();
            _statusesRepository = new StatusesRepository();
        }

        // GET api/samples
        public IEnumerable<SampleDto> Get()
        {
            var samples = _samplesRepository.GetSamples();
            var users = _usersRepository.GetUsers();
            var statuses = _statusesRepository.GetStatuses();
            var dtos = from s in samples
                join u in users on s.CreatedBy equals u.UserId
                join st in statuses on s.StatusId equals st.StatusId
                select new SampleDto
                    {
                        SampleId = s.SampleId,
                        Barcode = s.Barcode,
                        CreatedAt = s.CreatedAt,
                        CreatedById = s.CreatedBy,
                        CreatedByFirstName = u.FirstName,
                        CreatedByLastName = u.LastName,
                        StatusId = s.StatusId,
                        Status = st.Status1
                    };

            return dtos;
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
