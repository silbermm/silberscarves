using SilberScarves.Models;
using SilberScarves.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SilberScarves.Repositories
{
    public class UserRepository : Repository<User>
    {
        private SilberScarvesDbContext context;

        public UserRepository()
        {
            this.context = new SilberScarvesDbContext();
        }

        public UserRepository(SilberScarvesDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> getAll()
        {
            return context.Users.Include("roles");
        }

        public User getById(long id)
        {
            return context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User add(User entity)
        {
            User u = context.Users.Add(entity);
            context.SaveChanges();
            return u;
        }

        public void update(User entity)
        {
            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void delete(User entity)
        {
            context.Users.Remove(entity);
            context.SaveChanges();
        }
    }
}