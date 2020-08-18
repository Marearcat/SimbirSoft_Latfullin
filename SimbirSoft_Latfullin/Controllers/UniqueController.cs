using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimbirSoft_Latfullin.Domain;
using SimbirSoft_Latfullin.Services.Unique;
using SimbirSoft_Latfullin.ViewModels.Unique;

namespace SimbirSoft_Latfullin.Controllers
{
    public class UniqueController : Controller
    {
        private readonly ILogger<UniqueController> _logger;
        private readonly IUniqueService _uniqueService;
        private ApplicationContext _context;

        public UniqueController(ILogger<UniqueController> logger, IUniqueService uniqueService, ApplicationContext context)
        {
            _context = context;
            _logger = logger;
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

        public ActionResult Details()
        {
            return View();
        }
    }
}
