using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneByGene.Api.Controllers;
using GeneByGene.Api.Models;
using GeneByGene.Api.Repositories;

namespace GeneByGene.Api.Tests.Controllers
{
    [TestClass]
    public class StatusesControllerTest
    {
        [TestMethod]
        public void GetStatuses_ShouldReturnStatuses()
        {
            // Arrange
            var statusesRepository = new MockStatusesRepository();
            var controller = new StatusesController(statusesRepository)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var response = controller.Get();

            // Assert
            Assert.IsNotNull(response);

            List<Status> statuses;
            Assert.IsTrue(response.TryGetContentValue(out statuses));
            Assert.AreEqual(statuses.Count, 4);
        }
    }

    public class MockStatusesRepository : IStatusesRepository
    {
        public IEnumerable<Status> GetStatuses()
        {
            var status = new List<Status> {
                new Status {
                    StatusId = 0,
                    Status1 = "Received"
                },
                new Status {
                    StatusId = 1,
                    Status1 = "Accessioning"
                },
                new Status {
                    StatusId = 2,
                    Status1 = "In Lab"
                },
                new Status {
                    StatusId = 3,
                    Status1 = "Report Generation"
                }};
            return status;
        }
    }
}
