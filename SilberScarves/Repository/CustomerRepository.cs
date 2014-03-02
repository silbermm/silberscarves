using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    public class CustomerRepository : Repository<Customer>
    {
        SilberScarvesDbContext context;

        public CustomerRepository(){
            context = new SilberScarvesDbContext();
        }

        public CustomerRepository(SilberScarvesDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> getAll()
        {
            return context.Customers.Include("address");
        }

        public Customer getById(long id)
        {
            return context.Customers.Include("address").Where(c => c.customerId == id).FirstOrDefault();
        }

        public Customer add(Customer entity)
        {
            Customer c = context.Customers.Add(entity);
            context.SaveChanges();
            return c;
        }

        public void update(Customer entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void delete(Customer entity)
        {
            context.Customers.Remove(entity);
            context.SaveChanges();
        }
    }
}