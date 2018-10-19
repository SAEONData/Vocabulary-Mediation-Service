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
            var data = GetDepartments();
            return new StandardVocabOutput { Items = GetParents(data.AsQueryable()) };
        }

        [HttpGet]
        [Route("flat")]
        public StandardVocabOutput ListFlat()
        {
            var items = new List<StandardVocabItem>();
            var data = GetDepartments();

            items = data
                .Select(x => new StandardVocabItem
                {
                    Id = x.id.ToString(),
                    Value = x.name,
                    AdditionalData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("parent", x.parent)
                    }
                })
                .ToList();

            return new StandardVocabOutput { Items = items };
        }

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetParents(IQueryable<DeptItem> data)
        {
            var result = new List<StandardVocabItem>();

            var parents = data.Where(x => x.parent == "");

            foreach (var parent in parents)
            {
                result.Add(new StandardVocabItem
                {
                    Id = parent.id.ToString(),
                    Value = parent.name,
                    Children = GetChildren(data, parent.name)
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
                    Id = child.id.ToString(),
                    Value = child.name,
                    Children = GetChildren(data, child.name)
                });
            }

            return result;
        }

        private List<DeptItem> GetDepartments()
        {
            var data = new List<DeptItem>();

            using (WebClient wc = new WebClient())
            {
                //Get departments
                var json = wc.DownloadString("http://app01.saeon.ac.za/portal/dev_new/inst.js");
                var tmp = JsonConvert.DeserializeObject<List<DeptItem>>(json);

                //Extract and add "parents"
                var parents = tmp
                    .Where(x => tmp.Count(y => y.name == x.parent) == 0)
                    .Select(x => x.parent.Trim())
                    .Distinct();

                data.AddRange(parents.OrderBy(x => x).Select(x => new DeptItem() { name = x, parent = "" }));

                //Format and order
                data.AddRange(tmp.Select(x => new DeptItem { name = x.name.Trim(), parent = x.parent.Trim() })
                    .OrderBy(x => x.parent).ThenBy(x => x.name));

                //Inject IDs
                for (var index = 0; index < data.Count; index++)
                {
                    data[index].id = (index + 1);
                }
            }

            return data;
        }
    }

    internal class DeptItem
    {
        public int id { get; set; }
        public string parent { get; set; }
        public string name { get; set; }
    }
}