using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GeneByGene.Api.Models;

namespace GeneByGene.Api.Repositories
{
    public interface IStatusesRepository
    {
        IEnumerable<Status> GetStatuses();
    }
    public class StatusesRepository : IStatusesRepository
    {
        public IEnumerable<Status> GetStatuses()
        {
            using (var context = new Entities())
            {
                return context.Statuses.ToList();
            }
        }
    }
}