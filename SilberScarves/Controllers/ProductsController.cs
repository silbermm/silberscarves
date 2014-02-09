using SilberScarves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SilberScarves.Controllers
{
    public class ProductsController : Controller
    {

        private Repository<ScarfItem> scarfRepo = new ScarfItemRepository();

        //
        // GET: /Products/
        public ActionResult Index()
        {
            
            IEnumerable<ScarfItem> scarves = scarfRepo.getAll();
            return View(scarves);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(String name, String description, String price)
        {
            ScarfItem scarf = new ScarfItem();

            bool error = false;

            if (name == null || name.Equals(""))
            {
                Console.Out.WriteLine("name is null or empty");
                ViewBag.ErrorName = "name is required"; 
                error = true;
            }
            else
            {
                scarf.name = name;
            }
            if (description == null || description.Equals(""))
            {
                ViewBag.ErrorDescription = "description is required"; error = true;
            }
            else
            {
                scarf.description = description;
            }
            if (price == null || price.Equals(""))
            {
                ViewBag.ErrorPrice = "price is required"; error = true;
            }
            else
            {
                try
                {
                    scarf.price = decimal.Parse(price);
                }catch (Exception e){
                    ViewBag.ErrorPrice = e.Message;
                    error = true;
                }
            }
            if (error)
            {
                ViewBag.Error = "Please fill out all required fields";
                return View(scarf);
            }
            else
            {
                // Insert the data and return to the Index view
                scarfRepo.add(scarf);
                return View("Index", scarfRepo.getAll());
            }

        }
	}
}