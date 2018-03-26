using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNetCoreCookies.Data
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MinLength(8)]
        public string Cookie { get; set; }

        [Required]
        [MinLength(3)]
        public string EmailAddress { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        [Required]
        public DateTime? ExpireAt { get; set; }
    }
}
