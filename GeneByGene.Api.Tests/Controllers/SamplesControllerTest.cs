using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using GeneByGene.Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneByGene.Api.Dtos;
using GeneByGene.Api.Models;
using GeneByGene.Api.Repositories;

namespace GeneByGene.Api.Tests.Controllers
{
    [TestClass]
    public class SamplesControllerTest
    {
        [TestMethod]
        public void GetSamples_ShouldReturnSampleDtos()
        {
            // Arrange
            var statusesRepository = new MockStatusesRepository();
            var usersRepository = new MockUsersRepository();
            var samplesRepository = new MockSamplesRepository();
            var controller = new SamplesController(statusesRepository, samplesRepository, usersRepository)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var response = controller.Get();

            // Assert
            Assert.IsNotNull(response);

            List<SampleDto> samples;
            Assert.IsTrue(response.TryGetContentValue(out samples));
            Assert.AreEqual(samples.Count, 1);
        }
    }

    public class MockSamplesRepository : ISamplesRepository
    {
        public IEnumerable<Sample> GetSamples()
        {
            var samples = new List<Sample> {
                new Sample
                {
                    SampleId = 1,
                    Barcode = "JVN001",
                    CreatedAt = DateTime.Now,
                    CreatedBy = 0,
                    StatusId = 0
                } };
            return samples;
        }

        public int Save(SampleDto sampleDto)
        {
            return 0;
        }
    }
}
