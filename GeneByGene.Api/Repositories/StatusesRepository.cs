using System.Collections.Generic;
using System.Linq;
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