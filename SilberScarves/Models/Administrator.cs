using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    [Table("Admins")]
    public class Administrator
    {
        [Key]
        public long AdminId { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public Boolean isActive { get; set; }

    }
}