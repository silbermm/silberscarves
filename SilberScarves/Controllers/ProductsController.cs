using SilberScarves.Models;
using SilberScarves.Models.Repository;
using SilberScarves.services;
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

        ProductsService service = new ProductsService();

        //
        // GET: /Products/
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<ScarfItem> scarves = service.scarfRepo.getAll();
            Customer customer = getCurrentUser();

            var CandP = new CustomerAndProducts();
            CandP.Customer = customer;
            ScarfOrder s = service.orderRepo.getCustomerCart(customer);
            CandP.Cart = service.orderRepo.getCustomerCart(customer);
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
                    service.scarfRepo.add(scarf);

                    IEnumerable<ScarfItem> scarves = service.scarfRepo.getAll();
                    Customer customer = getCurrentUser();
                    var CandP = new CustomerAndProducts();
                    CandP.Customer = customer;
                    CandP.Cart = service.orderRepo.getCustomerCart(customer);
                    CandP.Scarves = scarves;
                    
                    return View("Index", CandP);
                }
            }
            else
            {
                TempData["GlobalError"] = "You are not Authorized to access the requested page";
                return RedirectToAction("Index", "Home");
            }


            

        }

        [HttpPost]
        public ActionResult Index(long scarfId)
        {
            Customer c = getCurrentUser();
            if (c == null)
            {
                // set an error message and return to view
                ViewBag.Error = "Please Login to add items to your cart!";

            }
            else
            {
                // user is logged in, lets add this item to their cart...
                var cart = service.orderRepo.getCustomerCart(c);
                if (cart != null)
                {
                    ScarfItem scarf = service.scarfRepo.getById(scarfId);
                    cart.Scarves.Add(scarf);
                }
                else
                {
                    ScarfOrder newOrder = new ScarfOrder(){ isCart = true, customer=c, hasBeenPaidFor=false, hasShipped =false };
                    ScarfItem scarf = service.scarfRepo.getById(scarfId);
                    newOrder.Scarves.Add(scarf);
                    service.orderRepo.add(newOrder);


                }

            }

            var CandP = new CustomerAndProducts();
            CandP.Customer = getCurrentUser();
            CandP.Scarves = service.scarfRepo.getAll();
            return View(CandP);
        }

        private Customer getCurrentUser()
        {
            Customer cust = null;
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                cust = service.custRepo.getAll().Where(c => c.username == this.HttpContext.User.Identity.Name).FirstOrDefault();
                return cust;
            }
            else
            {
                return null;
            }


        }
	}
}