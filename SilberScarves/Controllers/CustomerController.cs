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
            return View();
        }

	}
}