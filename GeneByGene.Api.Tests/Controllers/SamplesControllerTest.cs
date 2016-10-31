using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        public void PostSample_ValidSample_ShouldSucceed()
        {
            // Arrange
            var statusesRepository = new MockStatusesRepository();
            var usersRepository = new MockUsersRepository();
            var samplesRepository = new MockSamplesRepository();
            var originalSamplesCount = samplesRepository.GetSamples().Count;
            var controller = new SamplesController(statusesRepository, samplesRepository, usersRepository)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var sampleToSave = new SampleDto
            {
                Barcode = "JVN002",
                CreatedAt = DateTime.Now,
                CreatedById = 0,
                StatusId = 2
            };

            // Act
            var response = controller.Post(sampleToSave);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreNotEqual(0, response.Content);
            var newSamples = samplesRepository.GetSamples();
            Assert.AreEqual(originalSamplesCount + 1, newSamples.Count);
        }

        [TestMethod]
        public void PostSample_MissingBarcode_ShouldReturnError()
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
            var sampleToSave = new SampleDto
            {
                CreatedAt = DateTime.Now,
                CreatedById = 0,
                StatusId = 2
            };

            // Act
            var response = controller.Post(sampleToSave);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);

            HttpError error;
            Assert.IsTrue(response.TryGetContentValue(out error));
            Assert.IsTrue(error.Message.Contains("Barcode is required"));
        }
    }

    public class MockSamplesRepository : ISamplesRepository
    {
        private readonly List<Sample> _samples;

        public MockSamplesRepository()
        {
            _samples = new List<Sample> {
                new Sample
                {
                    SampleId = 0,
                    Barcode = "JVN001",
                    CreatedAt = DateTime.Now,
                    CreatedBy = 0,
                    StatusId = 0
                } };
        }
        public List<Sample> GetSamples()
        {
            return _samples;
        }

        public int Save(SampleDto sampleDto)
        {
            var sample = new Sample
            {
                SampleId = _samples.Count,
                Barcode = sampleDto.Barcode,
                CreatedAt = sampleDto.CreatedAt,
                CreatedBy = sampleDto.CreatedById,
                StatusId = sampleDto.StatusId
            };
            _samples.Add(sample);

            return sample.SampleId;
        }
    }
}
