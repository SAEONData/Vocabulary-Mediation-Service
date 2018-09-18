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
    [Route("api/Hazards")]
    public class HazardsController : Controller
    {
        public SQLDBContext _context { get; }
        public HazardsController(SQLDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public StandardVocabOutput List()
        {
            return new StandardVocabOutput { Items = GetHazards(_context.Hazards) };
        }

        [Route("Find/{find}")]
        [HttpGet]
        public StandardVocabOutput Find(string find)
        {
            //Build filtered data cache
            var dataCache = new List<Hazard>();

            var hazards = _context.Hazards
                .Include(x => x.HazardType)
                .Include(x => x.Parent)
                .Where(x => find == "" || x.Value.ToLower().Contains(find.ToLower()));

            dataCache.AddRange(hazards.ToList());

            bool foundParents = true;
            while (foundParents)
            {
                foundParents = false;
                var copyCache = new Hazard[dataCache.Count];
                Array.Copy(dataCache.ToArray(), copyCache, dataCache.Count);
                foreach (var hazard in copyCache)
                {
                    if (hazard.Parent != null && !hazards.Any(x => x.Id == hazard.Parent.Id))
                    {
                        var parents = _context.Hazards
                                .Include(x => x.HazardType)
                                .Include(x => x.Parent)
                                .Where(x => x.Id == hazard.Parent.Id);

                        foreach (var parent in parents)
                        {
                            if (!dataCache.Any(x => x.Id == parent.Id))
                            {
                                dataCache.Add(parent);
                                foundParents = true;
                            }
                        }
                    }
                }
            }

            return new StandardVocabOutput { Items = GetHazards(dataCache.AsQueryable()) };
        }

        //Recursively get Hazards and their 'children'
        private List<StandardVocabItem> GetHazards(IQueryable<Hazard> data)
        {
            var result = new List<StandardVocabItem>();

            var hazards = data         
                .Where(x => x.Parent == null)
                .OrderBy(x => x.Value);

            foreach (var hazard in hazards)
            {
                result.Add(new StandardVocabItem
                {
                    Id = hazard.Id.ToString(),
                    Value = hazard.Value,
                    Children = GetChildren(data, hazard.Id)
                });
            }

            return result;
        }

        //Recursively get Hazards and their 'children'
        private List<StandardVocabItem> GetChildren(IQueryable<Hazard> data, int parentId)
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
                    Children = GetChildren(data, child.Id)
                });
            }

            return result;
        }
    }
}