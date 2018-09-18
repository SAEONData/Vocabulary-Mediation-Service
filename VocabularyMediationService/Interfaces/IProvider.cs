using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyMediationService.Interfaces
{
    public interface IProvider
    {
        Task<object> Search(string searchPhrase);
        Task<object> View(string identifier);
    }
}
