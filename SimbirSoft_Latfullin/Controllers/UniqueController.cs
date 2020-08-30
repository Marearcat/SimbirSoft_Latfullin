using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimbirSoft_Latfullin.Services.Unique;
using SimbirSoft_Latfullin.ViewModels.Unique;
using System.Threading.Tasks;

namespace SimbirSoft_Latfullin.Controllers
{
    public class UniqueController : Controller
    {
        private readonly IUniqueService _uniqueService;

        public UniqueController(IUniqueService uniqueService)
        {
            _uniqueService = uniqueService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RequestedUri uri)
        {
            return View(await _uniqueService.GetTextFromPage(uri.Value));
        }

        public async Task<ActionResult> Example()
        {
            return View(await _uniqueService.GetExample());
        }
    }
}
