﻿using SilberScarves.Models;
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
            Customer customer = accountService.findCustomer(username);
            if (customer == null)
            {
                ViewBag.Error = "Wrong username or password";
                return View();
            }
            if (customer.password == password)
            {
                // successful!\
                FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }


	}
}