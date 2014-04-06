using System.Web.Routing;
using SilberScarves.Models;
using SilberScarves.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SilberScarves.Controllers
{
    public class AuthController : Controller
    {

        private AccountService accountService = new AccountService();

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
            
           SilberScarves.Models.Security.User user = accountService.findUser(username);
           if (user != null)
           {
               if (user.Password == password)
               {
                   FormsAuthentication.SetAuthCookie(username, false);                                
                   return RedirectToAction("Index", "Public", new {area = ""});
               }
               else
               {
                   ViewBag.Error = "Wrong username or password";
                   return View();
               }
           }
           else
           {
               ViewBag.Error = "Wrong username or password";
               return View();
           }         
            
        }

        public ActionResult Logout()
        {
            //var username = this.HttpContext.User.Identity.Name;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Public");
        }


	}
}