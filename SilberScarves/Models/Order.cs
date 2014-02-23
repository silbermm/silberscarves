using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    [Table("Orders")]
    public class Order
    {

        public Order(){
            customer = new Customer();
        }

        [Key]
        public long orderId { get; set; }
        public bool hasShipped { get; set; }
        public bool hasBeenPaidFor { get; set; }
        public Customer customer { get; set; }

    }
}