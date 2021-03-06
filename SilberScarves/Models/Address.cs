﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    [Table("Address")]
    public class Address
    {

        [Key]
        public long addressId { get; set; }
        public String buildingNumber { get; set; }
        public String street { get; set; }
        public String apartmentNumber { get; set; }
        public String city { get; set; }
        public String stateCode { get; set; }
        public String zipCode { get; set; }
     
    }
}