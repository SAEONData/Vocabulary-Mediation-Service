using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyMediationService.Database.Models
{
    public class Hazard
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public HazardType HazardType { get; set; }
        public Hazard Parent { get; set; }
    }
}
