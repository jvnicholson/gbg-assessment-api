using System;

namespace GeneByGene.Api.Dtos
{
    public class SampleDto
    {
        public int SampleId { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}