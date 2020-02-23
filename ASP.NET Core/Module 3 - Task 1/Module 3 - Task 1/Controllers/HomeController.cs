﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Module_3___Task_1.Models;
using Module3;
using System.Linq;

namespace Module_3___Task_1.Controllers
{
    public class HomeController : Controller
    {
        private NorthwindContext _context;

        public HomeController(NorthwindContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult Category()
        {
            var categorieses = this._context.Categories.ToList();
            return this.View(categorieses);
        }
    }
}
