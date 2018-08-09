using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using VocabularyMediationService.Interfaces;
using VocabularyMediationService.Models;

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
                //result = jobj;

                var parsedResult = jobj["result"]
                    .Select(x => new StandardVocabItem()
                    {
                        UID = x["orcid-identifier"]["path"].ToString(),
                        Value = x["orcid-identifier"]["path"].ToString(),
                        AdditionalData = new List<StandardVocabAdditionalData>()
                            {
                                new StandardVocabAdditionalData()
                                {
                                    Key = "link",
                                    Value = x["orcid-identifier"]["uri"].ToString()
                                }/*,
                                new StandardVocabAdditionalData()
                                {
                                    Key = "host",
                                    Value = x["orcid-identifier"]["host"].ToString()
                                }*/
                            }
                    })
                    .OrderBy(x => x.Value)
                    .ToList();

                //Convert to Object
                result = new StandardVocabOutput() { Items = parsedResult };
            }

            return result;
        }

        public async Task<object> View(string identifier)
        {
            object result = "";

            //Call API
            HttpResponseMessage response = await _client.GetAsync($"https://pub.orcid.org/v2.1/{identifier}");

            //Parse response
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();

                //Convert to Json
                var jobj = JObject.Parse(responseStr);

                result = jobj;
            }

            return result;
        }
    }
}
