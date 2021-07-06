using Microsoft.EntityFrameworkCore;
using PartsPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartsPractice.Models.DataLayer
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Parts> PartsItems { get; set; }

    }
}
