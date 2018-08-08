using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using VocabularyMediationService.Interfaces;
using VocabularyMediationService.Models.Re3DataModels;

namespace VocabularyMediationService.Providers
{
    public class ProviderRe3Data : IProvider
    {
        private HttpClient _client { get; }

        public ProviderRe3Data()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        }

        public async Task<object> Search(string searchPhrase)
        {
            object result = "";

            //Call API
            HttpResponseMessage response = await _client.GetAsync($"https://www.re3data.org/api/beta/repositories?query={searchPhrase}");

            //Parse response
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();

                //Load XML
                var doc = new XmlDocument();
                doc.LoadXml(responseStr);

                //Convert to Json
                var jstr = JsonConvert.SerializeXmlNode(doc);
                var jobj = JObject.Parse(jstr);

                result = jobj;

                //Convert to Object
                //result = JsonConvert.DeserializeObject<Repositories>(jstr);
            }

            return result;
        }

        public async Task<object> View(string identifier)
        {
            object result = "";

            //Call API
            HttpResponseMessage response = await _client.GetAsync($"https://www.re3data.org/api/beta/repository/{identifier}");

            //Parse response
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();

                //Load XML
                var doc = new XmlDocument();
                doc.LoadXml(responseStr);

                //Convert to Json
                var jstr = JsonConvert.SerializeXmlNode(doc);
                var jobj = JObject.Parse(jstr);

                result = jobj;
            }

            return result;
        }
    }
}
