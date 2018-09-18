using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyMediationService.Database;
using VocabularyMediationService.Database.Models;
using VocabularyMediationService.Models;

namespace VocabularyMediationService.Controllers
{
    [Produces("application/json")]
    [Route("api/Sectors")]
    public class SectorsController : Controller
    {
        public SQLDBContext _context { get; }
        public SectorsController(SQLDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public StandardVocabOutput List()
        {
            return new StandardVocabOutput { Items = GetSectors(_context.Sectors) };
        }

        [Route("Find/{find}")]
        [HttpGet]
        public StandardVocabOutput Find(string find)
        {
            //Build filtered data cache
            var dataCache = new List<Sector>();

            var sectors = _context.Sectors
                .Include(x => x.SectorType)
                .Include(x => x.ParentSector)
                .Where(x => find == "" || x.Value.ToLower().Contains(find.ToLower()));

            dataCache.AddRange(sectors.ToList());

            bool foundParents = true;
            while (foundParents)
            {
                foundParents = false;
                var copyCache = new Sector[dataCache.Count];
                Array.Copy(dataCache.ToArray(), copyCache, dataCache.Count);
                foreach (var sector in copyCache)
                {
                    if (sector.ParentSector != null && !sectors.Any(x => x.Id == sector.ParentSector.Id))
                    {
                        var parents = _context.Sectors
                                .Include(x => x.SectorType)
                                .Include(x => x.ParentSector)
                                .Where(x => x.Id == sector.ParentSector.Id);

                        foreach (var parent in parents)
                        {
                            if (!dataCache.Any(x => x.Value == parent.Value))
                            {
                                dataCache.Add(parent);
                                foundParents = true;
                            }
                        }
                    }
                }
            }

            return new StandardVocabOutput { Items = GetSectors(dataCache.AsQueryable()) };
        }

        [Route("{id}")]
        [HttpGet]
        public Sector Details(string id)
        {
            int.TryParse(id, out int parsedId);
            return _context.Sectors
                .Include(x => x.SectorType)
                .Include(x => x.ParentSector)
                .Include(x => x.ParentSector.SectorType)
                .Include(x => x.ParentSector.ParentSector)
                .Include(x => x.ParentSector.ParentSector.SectorType)
                .FirstOrDefault(x => x.Id == parsedId);
        }

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetSectors(IQueryable<Sector> data)
        {
            var result = new List<StandardVocabItem>();

            var sectors = data
                .Where(x => x.ParentSector == null)
                .OrderBy(x => x.Value);

            foreach (var sector in sectors)
            {
                result.Add(new StandardVocabItem
                {
                    Id = sector.Id.ToString(),
                    Value = sector.Value,
                    Children = GetChildren(data, sector.Id)
                });
            }

            return result;
        }

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetChildren(IQueryable<Sector> data, int parentId)
        {
            var result = new List<StandardVocabItem>();

            var children = data
                            .Where(x => x.ParentSector != null && x.ParentSector.Id == parentId)
                            .OrderBy(x => x.Value);

            foreach (var child in children)
            {
                result.Add(new StandardVocabItem
                {
                    Id = child.Id.ToString(),
                    Value = child.Value,
                    Children = GetChildren(data, child.Id)
                });
            }

            return result;
        }
    }
}