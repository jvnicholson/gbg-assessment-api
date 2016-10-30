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
