using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleRazorPages0.Data;

namespace SampleRazorPages0.Pages.Customers
{
    public class NewModel : PageModel
    {
        private readonly AppDbContext _db;

        public NewModel(AppDbContext db)
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



        public void OnGet()
        {

        }

        // the methods can also be bound
        //public void OnPost(Customer customer)
        //{
        //}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();

            MessageToDisplay = "Customer succesfully created";

            return RedirectToPage("Index");
        }
    }
}