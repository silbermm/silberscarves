using System.EnterpriseServices;
using Antlr.Runtime;
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
            IEnumerable<ScarfItem> scarves = service.getAllScarves();
            Customer customer = getCurrentUser();
            var CandP = new CustomerAndProducts();
            CandP.Customer = customer;
            ScarfOrder s = service.getCustomerCart(customer);           
            CandP.Cart = s;
            CandP.Scarves = scarves;
            return View(CandP);
        }

        [HttpGet]
        public ActionResult Scarf(int id)
        {
            Customer customer = getCurrentUser();
            ScarfItem scarf = service.getScarf(id);     
            var CandP = new CustomerAndProducts();
            CandP.Customer = customer;
            ScarfOrder s = service.getCustomerCart(customer);
            CandP.Cart = s;
            CandP.Scarves = new ScarfItem[]{scarf};
            
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
                    service.addScarf(scarf);

                    IEnumerable<ScarfItem> scarves = service.getAllScarves();
                    Customer customer = getCurrentUser();
                    var CandP = new CustomerAndProducts();
                    CandP.Customer = customer;
                    CandP.Cart = service.getCustomerCart(customer);
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
            ScarfOrder currentCart;
            if (c == null)
            {
                // set an error message and return to view
                ViewBag.Error = "Please Login to add items to your cart!";
                currentCart = service.EmptyCart();
            }
            else
            {
                // user is logged in, lets add this item to their cart...
                currentCart = service.getCustomerCart(c);
                ScarfItem scarf = service.getScarf(scarfId);
                currentCart.Scarves.Add(scarf);
                service.updateOrder(currentCart);               
                    /*
                    currentCart = service.EmptyCart(c);
                    ScarfItem scarf = service.getScarf(scarfId);
                    currentCart.Scarves.Add(scarf);
                    service.addOrder(currentCart);
                     */                              
            }

            var CandP = new CustomerAndProducts();
            CandP.Customer = c;
            CandP.Scarves = service.getAllScarves();
            CandP.Cart = currentCart;
            return View(CandP);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Checkout()
        {
            Customer c = getCurrentUser();
            ScarfOrder order = service.getCustomerCart(c);

            if (order == null)
            {
                TempData["GlobalError"] = "Your currently don't have any carts to checkout";
                return View("Index");
            }

            if (order.hasBeenPaidFor || order.hasShipped)
            {
                TempData["GlobalError"]="This order is already shipped";
            }

            // Confirm Shipping address
            return View("ConfirmAddress", c);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ConfirmAddress()
        {
            Customer c = getCurrentUser();
            ScarfOrder order = service.getCustomerCart(c);
            ViewBag.TotalCost = (Double) order.Scarves.Sum(s => s.price);
            order.hasBeenPaidFor = true;
            order.isCart = false;
            service.updateOrder(order);
            return View("Checkout");
        }

       

        private Customer getCurrentUser()
        {
            
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                return service.findCustomerByUsername(this.HttpContext.User.Identity.Name);
            }
            else
            {
                return null;
            }


        }
	}
}