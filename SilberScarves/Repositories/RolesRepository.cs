using SilberScarves.Models;
using SilberScarves.Models.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SilberScarves.Repositories
{
    public class RolesRepository : Repository<Roles>
    {
        private SilberScarvesDbContext context;

        public RolesRepository()
        {
            this.context = new SilberScarvesDbContext();
        }

        public RolesRepository(SilberScarvesDbContext context)
        {
            this.context = context;
        }


        public IEnumerable<Roles> getAll()
        {
            return context.Roles.Include("users");
        }

        public Roles getById(long id)
        {
            return context.Roles.Where(r => r.Id == id).FirstOrDefault();
        }

        public Roles add(Roles entity)
        {
            Roles r = context.Roles.Add(entity);
            context.SaveChanges();
            return r;
        }

        public void update(Roles entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void delete(Roles entity)
        {
            context.Roles.Remove(entity);
            context.SaveChanges();
        }
    }
}