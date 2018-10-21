using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyMediationService.Database.Models
{
    //South African Government Departments
    public class SAGovDept
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public SAGovDept Parent { get; set; }
    }
}
