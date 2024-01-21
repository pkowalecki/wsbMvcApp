using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wsbMvcApp.Models;

namespace wsbMvcApp.Data
{
    public class wsbMvcAppContext : DbContext
    {
        public wsbMvcAppContext (DbContextOptions<wsbMvcAppContext> options)
            : base(options)
        {
        }

        public DbSet<Meal> Meal { get; set; } = default!;

        public DbSet<wsbMvcApp.Models.User> User { get; set; } = default!;

    }
}
