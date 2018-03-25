using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleRazorPages0.Data;

namespace SampleRazorPages0.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db)
        {
            this._db = db;
            //Customers = new List<Customer>
            //{
            //    new Customer{FirstName="Heniu", LastName="Voight", BirthDate = DateTime.Now},
            //    new Customer{FirstName="Delta", LastName="Slavic", BirthDate = DateTime.Now.AddDays(-1)},
            //    new Customer{FirstName="Mandy", LastName="Sandy", BirthDate = DateTime.Now.AddDays(-600)}
            //};
        }
        public IList<Customer> Customers { get; set; }

        public void OnGet()
        {
            Customers =  _db.Customers.ToList();//.ToListAsync();
        }
    }
}