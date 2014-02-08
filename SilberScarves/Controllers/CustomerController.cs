using SilberScarves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SilberScarves.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        public ActionResult Index()
        {
            Repository<Customer> customerRepo = new CustomerRepository(); 
            return View(customerRepo.getAll());
        }
	}
}