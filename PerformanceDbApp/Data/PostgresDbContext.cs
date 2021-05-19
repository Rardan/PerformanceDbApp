using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Data
{
    public sealed class PostgresDbContext : AbstractDbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) 
            : base(options)
        {
        }
    }
}
