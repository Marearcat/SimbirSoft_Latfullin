using SimbirSoft_Latfullin.ViewModels.Unique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimbirSoft_Latfullin.Services.Unique
{
    public interface IUniqueService
    {
        public Task<UriResult> GetTextFromPage(string uri);
        public Task<List<UriResult>> GetExample();
    }
}
