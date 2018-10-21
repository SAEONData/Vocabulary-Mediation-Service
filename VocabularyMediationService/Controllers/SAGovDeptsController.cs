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
            var data = _context.SAGovDepts;
            return new StandardVocabOutput { Items = GetParents(data) };
        }

        [HttpGet]
        [Route("flat")]
        public StandardVocabOutput ListFlat()
        {
            var items = _context.SAGovDepts
                .Select(x => new StandardVocabItem
                {
                    Id = x.Id.ToString(),
                    Value = x.Value,
                    AdditionalData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("parent", x.Parent.Id.ToString())
                    }
                })
                .ToList();

            return new StandardVocabOutput { Items = items };
        }

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetParents(IQueryable<SAGovDept> data)
        {
            var result = new List<StandardVocabItem>();

            var depts = data.Where(x => x.Parent == null);

            foreach (var dept in depts)
            {
                result.Add(new StandardVocabItem
                {
                    Id = dept.Id.ToString(),
                    Value = dept.Value,
                    Children = GetChildren(data, dept.Id)
                });
            }

            return result;
        }

        //Recursively get Sectors and their 'children'
        private List<StandardVocabItem> GetChildren(IQueryable<SAGovDept> data, int parentId)
        {
            var result = new List<StandardVocabItem>();

            var depts = data
                        .Where(x => x.Parent.Id == parentId)
                        .OrderBy(x => x.Value);

            foreach (var dept in depts)
            {
                result.Add(new StandardVocabItem
                {
                    Id = dept.Id.ToString(),
                    Value = dept.Value,
                    Children = GetChildren(data, dept.Id)
                });
            }

            return result;
        }
    }
}