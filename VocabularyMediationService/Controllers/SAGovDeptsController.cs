using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VocabularyMediationService.Database;
using VocabularyMediationService.Database.Models;
using VocabularyMediationService.Models;

namespace VocabularyMediationService.Controllers
{
    [Produces("application/json")]
    [Route("api/SAGovDepts")]
    public class SAGovDeptsController : Controller
    {
        public SQLDBContext _context { get; }
        public SAGovDeptsController(SQLDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public StandardVocabOutput List()
        {
            var data = new List<DeptItem>();
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://app01.saeon.ac.za/portal/dev_new/inst.js");
                data = JsonConvert.DeserializeObject<List<DeptItem>>(json);
            }

            return new StandardVocabOutput { Items = GetParents(data.AsQueryable()) };
        }

        [HttpGet]
        [Route("flat")]
        public StandardVocabOutput ListFlat()
        {
            var items = new List<StandardVocabItem>();
            //var data = new List<DeptItem>();
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://app01.saeon.ac.za/portal/dev_new/inst.js");
                var data = JsonConvert.DeserializeObject<List<DeptItem>>(json);
                items = data.Select(x => new StandardVocabItem {
                    Id = x.name,
                    Value = x.name,
                    AdditionalData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("parent", x.parent)
                    }
                }).ToList();
            }

            return new StandardVocabOutput { Items = items };
        }

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetParents(IQueryable<DeptItem> data)
        {
            var result = new List<StandardVocabItem>();

            var parents = data
                .Where(x => data.Count(y => y.name == x.parent) == 0)
                .Select(x => x.parent)
                .Distinct()
                .OrderBy(x => x);

            foreach (var parent in parents)
            {
                result.Add(new StandardVocabItem
                {
                    Id = parent, //Guid.NewGuid().ToString(),
                    Value = parent,
                    Children = GetChildren(data, parent)
                });
            }

            return result;
        }

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetChildren(IQueryable<DeptItem> data, string parent)
        {
            var result = new List<StandardVocabItem>();

            var children = data
                            .Where(x => x.parent == parent && x.parent != x.name)
                            .OrderBy(x => x.name);

            foreach (var child in children)
            {
                result.Add(new StandardVocabItem
                {
                    Id = child.name, //Guid.NewGuid().ToString(),
                    Value = child.name,
                    Children = GetChildren(data, child.name)
                });
            }

            return result;
        }
    }

    internal class DeptItem
    {
        public string parent { get; set; }
        public string name { get; set; }
    }
}