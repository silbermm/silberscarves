using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilberScarves.Models
{
    public interface Repository<T>
    {
        IEnumerable<T> getAll();
        T getById(long id);
        T add(T entity);
        void update(T entity);
        void delete(T entity);
    }
}
