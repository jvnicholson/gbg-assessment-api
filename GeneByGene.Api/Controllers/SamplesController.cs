using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GeneByGene.Api.Dtos;
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
        public HttpResponseMessage Get()
        {
            try
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

                return Request.CreateResponse(HttpStatusCode.OK, sampleDtos);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    "There was a problem getting you that data.");
            }
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

        // POST api/samples
        public HttpResponseMessage Post([FromBody]SampleDto sample)
        {
            try
            {
                if (string.IsNullOrEmpty(sample.Barcode))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Barcode is required.");
                }
                if (sample.CreatedById < 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CreatedById must be greater than zero.");
                }
                if (sample.StatusId < 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "StatusId must be greater than zero.");
                }
                sample.CreatedAt = DateTime.Now;
                sample.SampleId = _samplesRepository.Save(sample);

                return Request.CreateResponse(HttpStatusCode.OK, sample);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    "There was a problem saving that Sample.");
            }
        }
    }
}
