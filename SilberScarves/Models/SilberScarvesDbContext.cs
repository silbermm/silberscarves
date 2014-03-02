using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    public class SilberScarvesDbContext : DbContext
    {
        public SilberScarvesDbContext()
        {
            this.Database.CreateIfNotExists();
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ScarfItem> Scarves { get; set; }
        public DbSet<ScarfOrder> Orders { get; set; }
    }
}
