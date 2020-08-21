using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimbirSoft_Latfullin.Services.Unique;
using SimbirSoft_Latfullin.ViewModels.Unique;

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
        public ActionResult Create(RequestedUri uri)
        {
            return View(_uniqueService.GetTextFromPage(uri.Value));
        }

        public ActionResult Example()
        {
            return View(_uniqueService.GetExample());
        }
    }
}
