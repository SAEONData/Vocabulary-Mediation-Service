using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VocabularyMediationService.Interfaces;
using VocabularyMediationService.Providers;

namespace VocabularyMediationService.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class LookupController : Controller
    {
        private IConfiguration _configuration;

        public LookupController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        [Route("services")]
        [HttpGet]
        public List<string> Services()
        {
            var supportedVocabServices = new List<string>()
            {
                "orcid",
                "re3data",
                "sagdad",
                "geonames",
                "obo",
                "worms"
            };

            return supportedVocabServices.OrderBy(x => x).ToList();
        }

        [Route("search/{providerName}/{searchPhrase}")]
        [HttpGet]
        public JsonResult Search(string providerName, string searchPhrase)
        {
            IProvider provider = GetProvider(providerName);
            return Json(provider.Search(searchPhrase).Result);
        }

        [Route("view/{providerName}/{identifier}")]
        [HttpGet]
        public JsonResult View(string providerName, string identifier)
        {
            IProvider provider = GetProvider(providerName);
            return Json(provider.View(identifier).Result);
        }


        //Helper Functions
        private IProvider GetProvider(string providerName)
        {
            switch (providerName.ToLower())
            {
                case "orcid":
                    return new ProviderOrcid();

                case "re3data":
                    return new ProviderRe3Data();

                default:
                    throw new ArgumentException($"Invalid provider name: {providerName}");
            }

        }
    }
}