using System.Collections.Generic;
using System.Linq;
using GeneByGene.Api.Models;

namespace GeneByGene.Api.Repositories
{
    public interface ISamplesRepository
    {
        IEnumerable<Sample> GetSamples();
    }
    public class SamplesRepository : ISamplesRepository
    {
        public IEnumerable<Sample> GetSamples()
        {
            using (var context = new Entities())
            {
                return context.Samples.ToList();
            }
        }
    }
}