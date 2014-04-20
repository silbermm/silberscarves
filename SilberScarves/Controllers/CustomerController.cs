using SilberScarves.Models;
using SilberScarves.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SilberScarves.Controllers

    
{
    public class CustomerController : Controller
    {

        private ProductsService productService = new ProductsService();
        private AccountService accountService = new AccountService();

        //
        // GET: /Customer/
        [Authorize]
        public ActionResult Index()
        {        
            return View(productService.getAllCustomers());
        }

        [Authorize]
        [HttpGet]
        public ActionResult Orders()
        {
            Customer c = accountService.findCustomer(this.HttpContext.User.Identity.Name);
            IEnumerable<ScarfOrder> orders = productService.getCustomerOrders(c);
            return View(orders);
        }

        [Authorize]
        public ActionResult Cart()
        {
            Customer c = accountService.findCustomer(this.HttpContext.User.Identity.Name);
            ScarfOrder cart = productService.getCustomerCart(c);
            return View(cart.Scarves);
        }

        [Authorize]
        public ActionResult Checkout() {
            var order = productService.getCustomerCart(getCurrentUser());
            return View(order);
        }

        [Authorize]
        [HttpGet]
        public ActionResult RemoveFromCart(long id)
        {
            Customer c = getCurrentUser();
            productService.RemoveFromCart(c, id);
            return RedirectToAction("Cart");
        }

        [Authorize]
        [HttpPost]
        public ActionResult FixAddress(Address address)
        {
            Customer c = getCurrentUser();
            c.address = address;
            accountService.updateAddress(address);
            return RedirectToAction("Checkout", "Products");
        }

        [HttpGet]
        [Authorize]
        public ActionResult FixAddress()
        {
            return View(getCurrentUser().address);
        }

        private Customer getCurrentUser()
        {

            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                return productService.findCustomerByUsername(this.HttpContext.User.Identity.Name);
            }
            else
            {
                return null;
            }


        }

	}
}