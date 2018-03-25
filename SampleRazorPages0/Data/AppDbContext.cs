using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleRazorPages0.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }

    public class Test
    {
        public void RunTest()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("inMem");
            var options = builder.Options;

            // SETUP TEST
            using (var appDb = new AppDbContext(options))
            {
                var customers = new[]
                {
                    new Customer{FirstName="Heniu", LastName="Voight", BirthDate = DateTime.Now},
                    new Customer{FirstName="Delta", LastName="Slavic", BirthDate = DateTime.Now.AddDays(-1)},
                    new Customer{FirstName="Mandy", LastName="Sandy", BirthDate = DateTime.Now.AddDays(-600)}
                };
                appDb.AddRange(customers);
            }

            // RUN TEST

            using (var context = new AppDbContext(options))
            {
                // run the test here!
            }


        }
    }
}
