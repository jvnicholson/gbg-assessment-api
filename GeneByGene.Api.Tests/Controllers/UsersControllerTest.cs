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
    public class UsersControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            var userRepository = new MockUserRepository();
            var controller = new UsersController(userRepository)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var response = controller.Get();

            // Assert
            Assert.IsNotNull(response);

            List<User> users;
            Assert.IsTrue(response.TryGetContentValue(out users));
            Assert.AreEqual(users.Count, 1);
        }
    }

    public class MockUserRepository : IUsersRepository
    {
        public IEnumerable<User> GetUsers()
        {
            var users = new List<User> {new User
                {
                    UserId = 100,
                    FirstName = "Josh",
                    LastName = "Nicholson"
                }};
            return users;
        }
    }
}
