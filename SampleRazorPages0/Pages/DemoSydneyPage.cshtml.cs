﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SampleRazorPages0.Pages
{
    public class DemoSydneyPageModel : PageModel
    {

        public string Message { get; set; }

        public void OnGet()
        {
            Message = $"{DateTime.Now} Hello Sydney OnGet() {nameof(DemoSydneyPageModel)}";
        }
    }
}