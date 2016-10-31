using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public HttpResponseMessage Get()
        {
            try
            {
                var users = _usersRepository.GetUsers();
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
