using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mattazeblog2021_site.Pages
{
    public class EditPostModel : PageModel
    {
        [BindProperty]
        public Models.PostItem PostItem { get; set; }

        public void OnGet() {
            PostItem = new Models.PostItem();
        }

        public void OnPost() {

        }

    }
}
