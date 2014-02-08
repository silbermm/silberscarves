using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    [Table("Customers")]
    public class Customer
    {
        public Customer()
        {
            address = new Address();
        }

        [Key]
        public long customerId { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public int addressId { get; set; }
        public String phone { get; set; }

        public Address address { get; set; }
    }
}