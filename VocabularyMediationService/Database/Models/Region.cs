using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyMediationService.Database.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Desription { get; set; }
        public RegionType Type { get; set; }
        public Region Parent { get; set; }
    }
}
