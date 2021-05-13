using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public double OrderTotal { get; set; }
        
        public List<int> OrderItems { get; set; }
        
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
