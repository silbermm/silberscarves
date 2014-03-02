using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    public class ScarfItemRepository : Repository<ScarfItem>
    {

        public ScarfItemRepository()
        {
            context = new SilberScarvesDbContext();
        }

        public ScarfItemRepository(SilberScarvesDbContext context)
        {
            this.context = context;
        }

        public SilberScarvesDbContext context;

        public IEnumerable<ScarfItem> getAll()
        {
            return context.Scarves;           
        }

        public ScarfItem getById(long id)
        {
            return context.Scarves.Where(s => s.scarfId == id).FirstOrDefault();
        }

        public ScarfItem add(ScarfItem entity)
        {
            ScarfItem si = context.Scarves.Add(entity);
            context.SaveChanges();
            return si;
        }

        public void update(ScarfItem entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void delete(ScarfItem entity)
        {
            context.Scarves.Remove(entity);
            context.SaveChanges();
        }
    }
}