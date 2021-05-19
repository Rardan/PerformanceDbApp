using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Data
{
    public sealed class MsDbContext : AbstractDbContext
    {
        public MsDbContext(DbContextOptions<MsDbContext> options) 
            : base(options)
        {
        }
    }
}
