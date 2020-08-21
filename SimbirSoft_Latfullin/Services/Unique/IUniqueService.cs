using SimbirSoft_Latfullin.ViewModels.Unique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimbirSoft_Latfullin.Services.Unique
{
    public interface IUniqueService
    {
        public UriResult GetTextFromPage(string uri);
        public List<UriResult> GetExample();
    }
}
