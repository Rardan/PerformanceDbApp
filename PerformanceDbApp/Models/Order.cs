using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Models
{
    public class Order : AbstractObject
    {
        public string Notes { get; set; }
        public double OrderTotal { get; set; }
        
        public List<OrderItem> OrderItems { get; set; }
    }
}
