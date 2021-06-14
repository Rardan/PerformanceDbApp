using PerformanceDbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Utils
{
    public static class PartialResultUtil
    {
        public static PartialResult CreatePartialResult(TimeResult r1, TimeResult r10, 
            TimeResult r20, TimeResult r50, TimeResult r100)
        {
            return new PartialResult() 
            {
                Result1 = r1,
                Result10 = r10,
                Result20 = r20,
                Result50 = r50,
                Result100 = r100
            };
        }

        public static TimeResult CreateTimeResult(IEnumerable<double> values)
        {
            return new TimeResult
            {
                Time = Math.Round(values.Average(), 3),
                Deviation = Math.Round(StatisticsUtil.StandardDeviation(values), 3)
            };
        }
    }
}
