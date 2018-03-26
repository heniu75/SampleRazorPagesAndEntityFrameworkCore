using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SampleRazorPages0.Data;

namespace SampleRazorPages0.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly MyCustomConfig config;

        public IndexModel(AppDbContext db, IOptions<MyCustomConfig> options)
        {
            this._db = db;
            this.config = options.Value;
        }

        public IList<Customer> Customers { get; set; }

        public bool HasCustomers => Customers.Any();

        // this is duplicated in the new page model
        [TempData]
        public string MessageToDisplay { get; set; }

        public bool ShowMessage => !String.IsNullOrEmpty(MessageToDisplay);

        public void OnGet()
        {
            Customers =  _db.Customers.ToList();//.ToListAsync();
        }

        // need to send the identifier of the thing to delete
        public async Task<IActionResult> OnPostDeleteAsync(int Id)
        {
            // use AsNoTracking() here for readonly queries
            var customerToDelete = _db.Customers
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == Id);

            if (customerToDelete == null)
            {
                // perhaps do some error handling here.
            }

            _db.Customers.Remove(customerToDelete);
            await _db.SaveChangesAsync();

            MessageToDisplay = $"Customer with Id '{Id}' deleted succesfully";

            // I have performed a post, so you should redirect to the page
            // (when no arguments are passed, it will redirect to the current page)
            return RedirectToPage();
        }
    }
}