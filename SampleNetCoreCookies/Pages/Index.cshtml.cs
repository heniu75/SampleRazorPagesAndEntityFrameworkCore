using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SampleNetCoreCookies.Pages
{
    [ServiceFilter(typeof(ClassConsoleLogActionOneFilter))]
    public class IndexModel : PageModel
    {
        [FromHeader]
        public string TempEmailAddress { get; set; }
        public void OnGet()
        {
            //TempEmailAddress
        }
    }
}
