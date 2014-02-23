using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    public class CustomerRepository : Repository<Customer>
    {
        SilberScarvesDbContext context = new SilberScarvesDbContext();

        public IEnumerable<Customer> getAll()
        {
            return context.Customers.Include("address");
        }

        public Customer getById(long id)
        {
            throw new NotImplementedException();
        }

        public Customer add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void delete(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}