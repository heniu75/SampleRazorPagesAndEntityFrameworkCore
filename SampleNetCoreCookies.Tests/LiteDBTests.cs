using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SampleNetCoreCookies.Tests
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Phones { get; set; }
        public bool IsActive { get; set; }
    }

    [TestClass]
    public class LiteDBTests
    {
        [TestMethod]
        public void LiteDBTests_Can_Insert_And_Find()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Get customer collection
                var customers = db.GetCollection<Customer>("customers");

                // Create your new customer instance
                var customer = new Customer
                {
                    Name = "John Doe",
                    Phones = new string[] { "8000-0000", "9000-0000" },
                    IsActive = true
                };

                var results = customers.Find(x => x.Name.StartsWith("Jo")).ToList();
                foreach(var result in results)
                {
                    customers.Delete(result.Id);
                }

                var currentCustomers = customers.FindAll().ToList();

                // Insert new customer document (Id will be auto-incremented)
                customers.Insert(customer);

                // Update a document inside a collection
                customer.Name = "Joana Doe";

                customers.Update(customer);

                // Index document using a document property
                customers.EnsureIndex(x => x.Name);

                // Use Linq to query documents
                var results1 = customers.Find(x => x.Name.StartsWith("Jo"));
                var resultsList = results1.ToList();
            }

        }
    }
}
