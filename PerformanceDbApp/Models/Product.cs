using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Models
{
    public class Product : AbstractObject
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Type { get; set; }
    }
}
