using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleRazorPages0.Data;

namespace SampleRazorPages0.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;

        public EditModel(AppDbContext db)
        {
            _db = db;
        }

        // this enables form binding on post and post-like verbs
        [BindProperty]
        public Customer Customer { get; set; }

        // Note this is duplicated in the new page model
        // this is for once-only-communication by the same user
        // i.e. this could persist over multiple requests 
        // until such a time the value is read by the actual user
        [TempData]
        public string MessageToDisplay { get; set; }

        public async Task<IActionResult> OnGetAsync(int Id)
        {
            Customer = await _db.Customers.FindAsync(Id);

            if (Customer == null)
            {
                MessageToDisplay = $"Customer {Id} not found!";
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentCustomer = _db.Customers.FirstOrDefault(x => x.Id == Customer.Id);

            if (currentCustomer == null)
            {
                // customer not found, do some error handling here
            }

            currentCustomer.FirstName = Customer.FirstName;
            currentCustomer.LastName = Customer.LastName;
            currentCustomer.BirthDate = Customer.BirthDate;
            currentCustomer.Email = Customer.Email;

            //_db.Customers.Add(Customer);
            await _db.SaveChangesAsync();

            MessageToDisplay = $"Customer with id {Customer.Id} succesfully updated";

            return RedirectToPage("Index");
        }
    }
}