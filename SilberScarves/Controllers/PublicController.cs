using SilberScarves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SilberScarves.Controllers
{
    public class PublicController : Controller
    {

        private static SilberScarvesDbContext context = new SilberScarvesDbContext();
        Repository<ScarfItem> scarfRepo = new ScarfItemRepository(context);

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
            
            IEnumerable<ScarfItem> scarves = scarfRepo.getAll();
            return View(scarves);
        }
    }
}