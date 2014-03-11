using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SilberScarves.Models.Security
{
    [Table("users")]
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public long CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer {get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
    }
}