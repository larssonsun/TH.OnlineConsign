using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace th.onlineconsign.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
            
        }

        public string ItemNameTitle { get; set; }

        public void OnGet()
        {
           
        }
    }
}
