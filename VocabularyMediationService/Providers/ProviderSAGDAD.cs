using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using VocabularyMediationService.Interfaces;
using VocabularyMediationService.Models;

namespace VocabularyMediationService.Providers
{
    public class ProviderSAGDAD : IProvider
    {
        private WebClient _client { get; }

        public ProviderSAGDAD()
        {
            _client = new WebClient();
        }

        public async Task<object> Search(string searchPhrase)
        {
            object result = "";

            //Read file
            var fileContent = "";
            using (Stream stream = _client.OpenRead("http://app01.saeon.ac.za/portal/www/sagdad.js"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    fileContent = await reader.ReadToEndAsync();
                }
            }
  
            if (!string.IsNullOrEmpty(fileContent))
            {
                //Parse to JObject
                var jstr = "{\"SAGDAD\": " + fileContent + "}";
                var jobj = JObject.Parse(jstr);

                //Filter and parse result
                var filteredItems = jobj["SAGDAD"]
                    .Where(x => x["text"].ToString().ToLower().Contains(searchPhrase.ToLower()))
                    .Select(x => new StandardVocabItem { UID = x["id"].ToString(), Value = x["text"].ToString() })
                    .OrderBy(x => x.Value)
                    .ToList();

                result = new StandardVocabOutput() { Items = filteredItems};
            }

            return result;
        }

        public async Task<object> View(string identifier)
        {
            return "NOT SUPPORTED";
        }
    }
}
