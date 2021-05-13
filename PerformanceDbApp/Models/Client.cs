using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
