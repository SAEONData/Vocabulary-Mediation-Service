using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyMediationService.Database.Models
{
    public class Sector
    {
        public int Id { get; set; }
        public string Value { get; set; }

        //FK - SectorType
        public SectorType SectorType { get; set; }

        //FK - ParentSector
        public Sector ParentSector { get; set; }
    }
}
