using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PremuimCalculator.Data;
using PremuimCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PremuimCalculator.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host,
                                            int? retry = 0) where TContext : PremuimDbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    InvokeDataSeeder(context);

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }
            return host;
        }

        private static void InvokeDataSeeder<TContext>(TContext context) where TContext: PremuimDbContext
        {
            if (!context.Occupations.Any())
            {
                var Occupations = new List<Occupation>
            {
                new Occupation { OccupationName = "Cleaner",Rating="Light Manual" },
                new Occupation { OccupationName = "Doctor",Rating="Professional" },
                new Occupation { OccupationName = "Author",Rating="White Collar" },
                new Occupation { OccupationName = "Farmer",Rating="Heavy Manual" },
                new Occupation { OccupationName = "Mechanic",Rating="Heavy Manual" },
                new Occupation { OccupationName = "Florist",Rating="Light Manual" },
            };
                context.AddRange(Occupations);
            }

            if (!context.OccupationRatings.Any())
            {
                var OccupationRatings = new List<OccupationRating>
            {
                new OccupationRating { Id=1, Factor= 1.50,Rating="Light Manual" },
                new OccupationRating {Id=2,Factor=1.0,Rating="Professional" },
                new OccupationRating {Id=3,Factor=1.25,Rating="White Collar" },
                new OccupationRating { Id=4,Factor=1.75,Rating="Heavy Manual" },
            };
                context.AddRange(OccupationRatings);             
            }
            context.SaveChanges();
        }
    }
}
