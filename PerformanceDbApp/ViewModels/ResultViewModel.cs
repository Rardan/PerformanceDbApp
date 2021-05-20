using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceDbApp.Models;

namespace PerformanceDbApp.ViewModels
{
    public class ResultViewModel
    {
        public string Name { get; set; }
        public PartialResult PostgreSQL { get; set; }
        public PartialResult MS_SQL { get; set; }
    }
}
