using SilberScarves.Models;
using SilberScarves.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SilberScarves.services
{
    public class ProductsService
    {

        private SilberScarvesDbContext context { get; set; }
        public Repository<ScarfItem> scarfRepo { get; set; }
        public Repository<Customer> custRepo { get; set; }
        public OrderRepository orderRepo { get; set; }


        public ProductsService()
        {
            context = new SilberScarvesDbContext();
            scarfRepo = new ScarfItemRepository(context);
            custRepo = new CustomerRepository();
            orderRepo = new OrderRepository(context);

        }


    }
}