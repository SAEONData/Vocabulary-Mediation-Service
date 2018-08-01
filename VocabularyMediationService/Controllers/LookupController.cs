using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public string Search(string providerName, string searchPhrase)
        {
            string result = "success";

            switch (providerName.ToLower())
            {
                case "orcid":
                    var provider = new ProviderOrcid("", "");
                    break;
            }

            return result; //$"Provider: {provider}, Phrase: {phrase}";
        }
    }
}