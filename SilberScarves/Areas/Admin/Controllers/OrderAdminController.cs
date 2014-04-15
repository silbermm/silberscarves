using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SilberScarves.Areas.Admin.Models;
using SilberScarves.services;
using SilberScarves.Models;

namespace SilberScarves.Areas.Admin.Controllers
{
    public class OrderAdminController : Controller
    {
        
        private ProductsService _productService = new ProductsService();

        //
        // GET: /Admin/OrderAdmin/
        public ActionResult Index()
        {
            var orderAdmin = new List<OrderAdmin>();
            List<ScarfOrder> orders = _productService.getCheckedOutOrders();
            foreach (var order in orders)
            {
                orderAdmin.Add(new OrderAdmin
                {
                   Customer = order.customer,
                   Order = order,
                   Total = order.Scarves.Sum(s => s.price)
                });
            }
            return View(orderAdmin);
        }

        public ActionResult Details(long id)
        {
            ScarfOrder s = _productService.getOrder(id);
            ViewBag.OrderId = id;
            return View(s.Scarves);
        }

        public ActionResult Ship(long id)
        {
            return View();
        }
    
    }

    
}