using System.Collections.Generic;
using System.Web.Http;
using GeneByGene.Api.Models;
using GeneByGene.Api.Repositories;

namespace GeneByGene.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController()
        {
            _usersRepository = new UsersRepository();
        }

        // GET api/users
        public IEnumerable<User> Get()
        {
            return _usersRepository.GetUsers();
        }
    }
}
