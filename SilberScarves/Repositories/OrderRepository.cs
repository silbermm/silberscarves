using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SilberScarves.Models.Repository
{
    public class OrderRepository : Repository<ScarfOrder>
    {

        public SilberScarvesDbContext context { get; set; }

        public OrderRepository()
        {
            context = new SilberScarvesDbContext();
        }

        public OrderRepository(SilberScarvesDbContext context){
            this.context = context;
        }

        

        public IEnumerable<ScarfOrder> getAll()
        {
            return context.Orders.Include("Customer");
        }

        public ScarfOrder getById(long id)
        {
            return context.Orders.Include("customer").Where(s => s.orderId == id).FirstOrDefault();
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
            ScarfOrder order = context.Orders.Where(o => o.customer.customerId == customer.customerId && o.isCart).FirstOrDefault();  
            return order;
            
        }
    }
}