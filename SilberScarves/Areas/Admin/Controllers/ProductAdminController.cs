using System.Web.Routing;
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

        [HttpGet()]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(long id)
        {
 
                try
                {
                    service.deleteScarf(id);
                }
                catch (Exception e)
                {
                    ViewBag.Error = "You tried to delete a scarf that doesn't exist";
                    return View("Index", service.getAllScarves());
                }
                return RedirectToAction("Index", "ProductAdmin", new {area = "Admin"});
               
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(long id)
        {
            try
            {
                var scarf = service.getScarf(id);
                return View(scarf);
            }
            catch (Exception e)
            {
                ViewBag.Error("There is no such scarf!");
                return View("Index", service.getAllScarves());
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(long id, String name, String description, String price)
        {
            var scarf = service.getScarf(id);

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
                service.updateScarf(scarf);
                IEnumerable<ScarfItem> scarves = service.getAllScarves();
                return View("Index", scarves);
            }     
        }

    }
}