using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    [Table("Scarves")]
    public class ScarfItem
    {
        [Key]
        public long scarfId { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public decimal price { get; set; }

    }
}