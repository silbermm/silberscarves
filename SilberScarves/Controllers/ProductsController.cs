using SilberScarves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SilberScarves.Controllers
{
    public class ProductsController : Controller
    {

        private Repository<ScarfItem> scarfRepo = new ScarfItemRepository();
        private Repository<Customer> custRepo = new CustomerRepository();

        //
        // GET: /Products/
        public ActionResult Index()
        { 
            IEnumerable<ScarfItem> scarves = scarfRepo.getAll();            
            var CandP = new CustomerAndProducts();
            CandP.Customer = getCurrentUser();
            CandP.Scarves = scarves;
            return View(CandP);
        }

        [Authorize]
        public ActionResult Create()
        {
            Customer c = getCurrentUser();
            if (c != null && c.isAdmin)
            {
                return View();
            }
            else
            {
                TempData["GlobalError"] = "You are not Authorized to access the requested page";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(String name, String description, String price)
        {

            Customer c = getCurrentUser();
            if (c != null && c.isAdmin)
            {
                return View();
            }
            else
            {
                TempData["GlobalError"] = "You are not Authorized to access the requested page";
                return RedirectToAction("Index", "Home");
            }


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

        [HttpPost]
        public ActionResult AddToCart(String jsonObject)
        {            
            Response.StatusCode = 400; // Replace .AddHeader
            var error = new Error();  // Create class Error() w/ prop
            error.Level = 2;
            error.Message = jsonObject;
            return Json(error, JsonRequestBehavior.AllowGet);

        }

        private Customer getCurrentUser()
        {
            Customer cust = null;
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                cust = custRepo.getAll().Where(c => c.username == this.HttpContext.User.Identity.Name).FirstOrDefault();
                return cust;
            }
            else
            {
                return null;
            }


        }
	}
}