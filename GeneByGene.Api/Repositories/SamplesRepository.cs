using System.Collections.Generic;
using System.Linq;
using GeneByGene.Api.Dtos;
using GeneByGene.Api.Models;

namespace GeneByGene.Api.Repositories
{
    public interface ISamplesRepository
    {
        List<Sample> GetSamples();
        int Save(SampleDto sample);
    }
    public class SamplesRepository : ISamplesRepository
    {
        public List<Sample> GetSamples()
        {
            using (var context = new Entities())
            {
                return context.Samples.ToList();
            }
        }

        public int Save(SampleDto sampleDto)
        {
            using (var context = new Entities())
            {
                var sample = new Sample
                {
                    Barcode = sampleDto.Barcode,
                    CreatedAt = sampleDto.CreatedAt,
                    CreatedBy = sampleDto.CreatedById,
                    StatusId = sampleDto.StatusId
                };
                context.Samples.Add(sample);
                context.SaveChanges();

                return sample.SampleId;
            }
        }
    }
}