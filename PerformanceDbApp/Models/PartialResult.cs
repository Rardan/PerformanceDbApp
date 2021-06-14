using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Models
{
    public class PartialResult
    {
        public TimeResult Result1 { get; set; }
        public TimeResult Result10 { get; set; }
        public TimeResult Result20 { get; set; }
        public TimeResult Result50 { get; set; }
        public TimeResult Result100 { get; set; }

    }
}
