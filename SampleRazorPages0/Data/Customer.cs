using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleRazorPages0.Data
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "First name")]
        [MinLength(2)]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "Last name")]
        [MinLength(2)]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
    }
}
