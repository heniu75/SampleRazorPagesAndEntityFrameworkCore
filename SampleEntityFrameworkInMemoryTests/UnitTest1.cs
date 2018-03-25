using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace SampleEntityFrameworkInMemoryTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
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
                appDb.SaveChanges();
            }

            // RUN TEST
            using (var context = new AppDbContext(options))
            {
                // run the test here!
                var repository = new CustomerRepository(context);

                // ASSERT
                Assert.AreEqual(3, repository.Get().Count());
                Assert.AreEqual(1,repository.Get().Count(x => x.FirstName == "Delta"));
                Assert.AreEqual(1, repository.Get().Count(x => x.LastName == "Voight"));
                Assert.AreEqual(1, repository.Get().Count(x => x.FirstName == "Mandy"));
            }
        }
    }
}
