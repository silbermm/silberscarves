using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    public class AddressRepository : Repository<Address>
    {


        SilberScarvesDbContext context { get; set; }

        public AddressRepository()
        {
            this.context = new SilberScarvesDbContext();
        }

        public AddressRepository(SilberScarvesDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Address> getAll()
        {
            return context.Addresses;
        }

        public Address getById(long id)
        {
            return context.Addresses.Where(p => p.addressId == id).FirstOrDefault();
        }

        public Address add(Address entity)
        {
            Address ent = context.Addresses.Add(entity);
            context.SaveChanges();
            return ent;

        }

        public void update(Address entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void delete(Address entity)
        {
            context.Addresses.Remove(entity);
            context.SaveChanges();
        }
    }
}