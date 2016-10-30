using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
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
            var users = _usersRepository.GetUsers();
            var statuses = _statusesRepository.GetStatuses();
            var sampleDtos = from s in _samplesRepository.GetSamples()
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

            var pairs = Request.GetQueryNameValuePairs();
            if (pairs.Any())
            {
                sampleDtos = filterSampleDtos(sampleDtos, pairs);
            }

            return sampleDtos;
        }

        private IEnumerable<SampleDto> filterSampleDtos(IEnumerable<SampleDto> samples, IEnumerable<KeyValuePair<string, string>> pairs)
        {
            foreach (var kvp in pairs)
            {
                if (kvp.Key == "statusId")
                {
                    int statusId;
                    if (int.TryParse(kvp.Value, out statusId))
                        samples = samples.Where(s => s.StatusId == statusId);
                }
                if (kvp.Key == "createdBy" && !string.IsNullOrWhiteSpace(kvp.Value))
                {
                    var nameStr = kvp.Value.ToLower();
                    samples =
                        samples.Where(
                            s => s.CreatedByFirstName.ToLower().Contains(nameStr) || s.CreatedByLastName.ToLower().Contains(nameStr));
                }
            }

            return samples;
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
