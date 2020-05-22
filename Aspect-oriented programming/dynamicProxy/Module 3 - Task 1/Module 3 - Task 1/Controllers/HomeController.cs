using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Module_3___Task_1.Models;
using Module3;
using System.Linq;

namespace Module_3___Task_1.Controllers
{
    using Castle.DynamicProxy;

    using WebShop.LoggingCastelCoreLib;

    public class HomeController : Controller
    {
        private ILogic _logic;
        private NorthwindContext _context;

        public HomeController(NorthwindContext context)
        {
            this._context = context;

            var generator = new ProxyGenerator();
            this._logic = generator.CreateInterfaceProxyWithTarget<ILogic>(new Logic(), new LogMethodCastelCore());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            this._logic.BusinessLogic();
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
