using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SimbirSoft_Latfullin.Controllers
{
    public class UniqueController : Controller
    {
        private readonly ILogger<UniqueController> _logger;

        public UniqueController(ILogger<UniqueController> logger)
        {
            _logger = logger;
        }

        // GET: UniqueController
        public ActionResult Index()
        {

            return View();
        }

        // GET: UniqueController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UniqueController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UniqueController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UniqueController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UniqueController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UniqueController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UniqueController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
