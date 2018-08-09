using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyMediationService.Models
{
    public class StandardVocabOutput
    {
        public List<StandardVocabItem> Items { get; set; }

        public StandardVocabOutput()
        {
            Items = new List<StandardVocabItem>();
        }
    }

    public class StandardVocabItem
    {
        public string UID { get; set; }
        public string Value { get; set; }

        //Optional
        public List<StandardVocabAdditionalData> AdditionalData { get; set; }

        public StandardVocabItem()
        {
            UID = "";
            Value = "";
            AdditionalData = new List<StandardVocabAdditionalData>();
        }
    }

    public class StandardVocabAdditionalData
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public StandardVocabAdditionalData()
        {
            Key = "";
            Value = "";
        }
    }
}
