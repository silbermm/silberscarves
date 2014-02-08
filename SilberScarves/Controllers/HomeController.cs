using SilberScarves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SilberScarves.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Scarves()
        {
            Repository<ScarfItem> scarfRepo = new ScarfItemRepository();
            IEnumerable<ScarfItem> scarves = scarfRepo.getAll();
            return View(scarves);
        }
    }
}