using PerformanceDbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Utils
{
    public static class PartialResultUtil
    {
        public static PartialResult CreatePartialResult(IEnumerable<double> values)
        {
            PartialResult result = new PartialResult
            {
                Time = Math.Round(values.Average(), 3),
                Deviation = Math.Round(StatisticsUtil.StandardDeviation(values), 3)
            };
            return result;
        }
    }
}
