using SilberScarves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SilberScarves.services;

namespace SilberScarves.Controllers
{
    public class PublicController : Controller
    {
        private ProductsService productsService = new ProductsService();
        //private static SilberScarvesDbContext context = new SilberScarvesDbContext();
        //Repository<ScarfItem> scarfRepo = new ScarfItemRepository(context);

        public ActionResult Index()
        {
            IEnumerable<ScarfItem> scarves = productsService.getAllScarves().Where(s => s.isFeatured);
            return View(scarves);
        }

        
    }
}