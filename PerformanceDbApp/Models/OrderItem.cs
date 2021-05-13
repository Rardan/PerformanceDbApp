using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
