using SilberScarves.Models;
using SilberScarves.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SilberScarves.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductAdminController : Controller
    {

        ProductsService service = new ProductsService();
        
        public ActionResult Index()
        {
            return View(service.getAllScarves());
        }

        public ActionResult Create()
        {
            return View(new ScarfItem());
        }

        [HttpPost]
        [Authorize(Roles="admin")]
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
                    }
                    catch (Exception e)
                    {
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
                    service.addScarf(scarf);
                    IEnumerable<ScarfItem> scarves = service.getAllScarves();  
                    return View("Index", scarves);
                }
            




        }
	}
}