using SilberScarves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SilberScarves.Controllers
{
    public class AuthController : Controller
    {

        private Repository<Customer> customerRepo = new CustomerRepository();

        public ActionResult Index()
        {
            return View("/Auth/Login");
        }

        //
        // GET: /Auth/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String username, String password)
        {
            return View("../Home/Index");
        }


	}
}