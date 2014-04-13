using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SilberScarves.Models;

namespace SilberScarves.Areas.Admin.Models
{
    public class OrderAdmin
    {
        public ScarfOrder Order { get; set; }
        public Customer Customer { get; set; }
        public Decimal Total { get; set; }

    }
}