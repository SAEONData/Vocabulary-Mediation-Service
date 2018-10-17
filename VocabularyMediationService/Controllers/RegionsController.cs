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
    [Route("api/Regions")]
    public class RegionsController : Controller
    {
        public SQLDBContext _context { get; }
        public RegionsController(SQLDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public StandardVocabOutput List()
        {
            return new StandardVocabOutput { Items = GetRegions(_context.Regions) };
        }

        [HttpGet]
        [Route("flat")]
        public StandardVocabOutput ListFlat()
        {
            var result = new StandardVocabOutput();
            var regions = _context.Regions
                .Include(x => x.Parent)
                .OrderBy(x => x.Value);

            foreach (var region in regions)
            {
                var stdVocabItem = new StandardVocabItem();
                stdVocabItem.Id = region.Id.ToString();
                stdVocabItem.Value = region.Value;

                if (region.Parent != null)
                {
                    stdVocabItem.AdditionalData.Add(new KeyValuePair<string, string>("ParentId", region.Parent.Id.ToString()));
                }

                result.Items.Add(stdVocabItem);
            }

            return result;
        }

        [Route("{id}")]
        [HttpGet]
        public Region Details(string id)
        {
            int.TryParse(id, out int parsedId);
            return _context.Regions
                .Include(x => x.Type)
                .Include(x => x.Parent)
                .FirstOrDefault(x => x.Id == parsedId);
        }

        [Route("Find/{find}")]
        [HttpGet]
        public StandardVocabOutput Find(string find)
        {
            //Build filtered data cache
            var dataCache = new List<Region>();

            var regions = _context.Regions
                .Include(x => x.Type)
                .Include(x => x.Parent)
                .Where(x => find == "" || x.Value.ToLower().Contains(find.ToLower()));

            dataCache.AddRange(regions.ToList());

            bool foundParents = true;
            while (foundParents)
            {
                foundParents = false;
                var copyCache = new Region[dataCache.Count];
                Array.Copy(dataCache.ToArray(), copyCache, dataCache.Count);
                foreach (var region in copyCache)
                {
                    if (region.Parent != null && !regions.Any(x => x.Id == region.Parent.Id))
                    {
                        var parents = _context.Regions
                                .Include(x => x.Type)
                                .Include(x => x.Parent)
                                .Where(x => x.Id == region.Parent.Id);

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

            return new StandardVocabOutput { Items = GetRegions(dataCache.AsQueryable()) };
        }

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetRegions(IQueryable<Region> data)
        {
            var result = new List<StandardVocabItem>();

            var regions = data
                .Where(x => x.Parent == null)
                .OrderBy(x => x.Value);

            foreach (var region in regions)
            {
                result.Add(new StandardVocabItem
                {
                    Id = region.Id.ToString(),
                    Value = region.Value,
                    Children = GetChildren(data, region.Id)
                });
            }

            return result;
        }

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetChildren(IQueryable<Region> data, int parentId)
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