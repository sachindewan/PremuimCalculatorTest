using Microsoft.EntityFrameworkCore;
using PremuimCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremuimCalculator.Data
{
    public class PremuimDbContext:DbContext
    {
        public PremuimDbContext(DbContextOptions<PremuimDbContext> dbContextOptions):base(dbContextOptions)
        {
                
        }

        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<OccupationRating> OccupationRatings { get; set; }

    }
}
