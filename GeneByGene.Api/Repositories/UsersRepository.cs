using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GeneByGene.Api.Models;

namespace GeneByGene.Api.Repositories
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetUsers();
    }
    public class UsersRepository : IUsersRepository
    {
        public IEnumerable<User> GetUsers()
        {
            using (var context = new Entities())
            {
                return context.Users.ToList();
            }
        }
    }
}