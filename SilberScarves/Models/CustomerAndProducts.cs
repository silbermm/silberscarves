using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SilberScarves.Models
{
    public class CustomerAndProducts
    {
        public Customer Customer {get; set;}
        public IEnumerable<ScarfItem> Scarves { get; set; }
        
    }
}