﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using VocabularyMediationService.Interfaces;
using VocabularyMediationService.Models.Orcid;

namespace VocabularyMediationService.Providers
{
    public class ProviderOrcid : IProvider
    {
        private HttpClient _client { get; }

        public ProviderOrcid()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.orcid+json"));
        }

        public async Task<object> Search(string searchPhrase)
        {
            object result = "";

            //Call API
            HttpResponseMessage response = await _client.GetAsync($"https://pub.orcid.org/v2.1/search?q=\"{searchPhrase}\"");

            //Parse response
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();

                //Convert to Json
                var jobj = JObject.Parse(responseStr);

                result = jobj;

                //Convert to Object
                //result = JsonConvert.DeserializeObject<SearchResult>(responseStr);
            }

            return result;
        }

        public async Task<object> View(string identifier)
        {
            object result = "";

            //Call API
            HttpResponseMessage response = await _client.GetAsync($"https://pub.orcid.org/v2.1/{identifier}"); //0000-0002-5662-263X

            //Parse response
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();

                //Convert to Json
                var jobj = JObject.Parse(responseStr);

                result = jobj;

                //Convert to Object
                //result = JsonConvert.DeserializeObject<Record>(responseStr);
            }

            return result;
        }
    }
}
