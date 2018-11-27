using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace VocabularyMediationService.Database.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Desription { get; set; }
        public RegionType Type { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        [IgnoreDataMember]
        public Region Parent { get; set; }

        //Additional fields added by Johan & Wim
        [Column(TypeName="text")]
        public string Source { get; set; }

        [Column(TypeName = "float")]
        public double? BoundX1 { get; set; }

        [Column(TypeName = "float")]
        public double? BoundY1 { get; set; }

        [Column(TypeName = "float")]
        public double? BoundX2 { get; set; }

        [Column(TypeName = "float")]
        public double? BoundY2 { get; set; }

        [Column(TypeName = "text")]
        public string WKT { get; set; }

        [Column(TypeName = "text")]
        public string SimpleWKT { get; set; }
    }
}
