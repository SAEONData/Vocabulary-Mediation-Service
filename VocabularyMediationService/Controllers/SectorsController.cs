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

        [Route("{id}")]
        [HttpGet]
        public Sector Details(string id)
        {
            int.TryParse(id, out int parsedId);
            return _context.Sectors
                .Include(x => x.SectorType)
                .Include(x => x.Parent)
                .Include(x => x.Parent.SectorType)
                .Include(x => x.Parent.Parent)
                .Include(x => x.Parent.Parent.SectorType)
                .FirstOrDefault(x => x.Id == parsedId);
        }

        [Route("Find/{find}")]
        [HttpGet]
        public StandardVocabOutput Find(string find)
        {
            //Build filtered data cache
            var dataCache = new List<Sector>();

            var sectors = _context.Sectors
                .Include(x => x.SectorType)
                .Include(x => x.Parent)
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
                    if (sector.Parent != null && !sectors.Any(x => x.Id == sector.Parent.Id))
                    {
                        var parents = _context.Sectors
                                .Include(x => x.SectorType)
                                .Include(x => x.Parent)
                                .Where(x => x.Id == sector.Parent.Id);

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

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetSectors(IQueryable<Sector> data)
        {
            var result = new List<StandardVocabItem>();

            var sectors = data
                .Where(x => x.Parent == null)
                .OrderBy(x => x.Value);

            foreach (var sector in sectors)
            {
                result.Add(new StandardVocabItem
                {
                    Id = sector.Id.ToString(),
                    Value = sector.Value,                    
                    Children = GetChildren(data, sector.Id),
                    AdditionalData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("SIC_Code", sector.SIC_Code)
                    }
                });
            }

            return result;
        }

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetChildren(IQueryable<Sector> data, int parentId)
        {
            var result = new List<StandardVocabItem>();

            var children = data
                            .Where(x => x.Parent != null && x.Parent.Id == parentId)
                            .OrderBy(x => x.Value);

            foreach (var child in children)
            {
                result.Add(new StandardVocabItem
                {
                    Id = child.Id.ToString(),
                    Value = child.Value,
                    Children = GetChildren(data, child.Id),
                    AdditionalData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("SIC_Code", child.SIC_Code)
                    }
                });
            }

            return result;
        }
    }
}