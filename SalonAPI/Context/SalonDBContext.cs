using Microsoft.EntityFrameworkCore;
using SalonAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Context
{
    public class SalonDBContext: DbContext
    {
        public DbSet<Salon> Salons { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public SalonDBContext(DbContextOptions<SalonDBContext> options) : base(options)
        {

        }
    }
}
