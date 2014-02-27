using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    [Table("Orders")]
    public class ScarfOrder
    {

        public ScarfOrder(){
            customer = new Customer();
            Scarves = new HashSet<ScarfItem>();
        }

        [Key]
        public long orderId { get; set; }
        public bool isCart { get; set; }
        public bool hasShipped { get; set; }
        public bool hasBeenPaidFor { get; set; }
      
        public virtual ICollection<ScarfItem> Scarves { get; set; }

        public Customer customer { get; set; }

    }
}