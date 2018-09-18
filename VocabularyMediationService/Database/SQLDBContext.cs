using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyMediationService.Database.Models;

namespace VocabularyMediationService.Database
{
    public class SQLDBContext : DbContext
    {
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<SectorType> SectorTypes { get; set; }
        public DbSet<Hazard> Hazards { get; set; }
        public DbSet<HazardType> HazardTypes { get; set; }

        public SQLDBContext() : base() { }

        public SQLDBContext(DbContextOptions options) : base(options) { }
    }
}
