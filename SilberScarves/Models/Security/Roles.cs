using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SilberScarves.Models.Security
{
    [Table("roles")]
    public class Roles
    {
        [Key]
        public int Id {get; set;}

        public string Name {get; set;}

        public virtual ICollection<User> Users { get; set; }
    }
}