using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SilberScarves.Models.Repository
{
    public class OrderRepository : Repository<ScarfOrder>
    {

        public OrderRepository(SilberScarvesDbContext context){
            this.context = context;
        }

        SilberScarvesDbContext context;

        public IEnumerable<ScarfOrder> getAll()
        {
            return context.Orders;
        }

        public ScarfOrder getById(long id)
        {
            return context.Orders.Where(s => s.orderId == id).FirstOrDefault();
        }

        public ScarfOrder add(ScarfOrder entity)
        {
            ScarfOrder si = context.Orders.Add(entity);
            context.SaveChanges();
            return si;
        }

        public void update(ScarfOrder entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void delete(ScarfOrder entity)
        {
            context.Orders.Remove(entity);
            context.SaveChanges();
        }

        public ScarfOrder getCustomerCart(Customer customer)
        {
            ScarfOrder order = context.Orders.Where(o => o.customer.customerId == customer.customerId).Where(o => o.isCart == true).FirstOrDefault();
            return order;
            
        }
    }
}