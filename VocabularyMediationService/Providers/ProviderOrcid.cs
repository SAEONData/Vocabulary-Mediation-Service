using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyMediationService.Providers
{
    public class ProviderOrcid
    {
        private string _clientId { get; }
        private string _clientSecret { get; }

        public ProviderOrcid(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }


    }
}
